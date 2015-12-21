using System;
using Java;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;

// Adapted from
// https://github.com/MikeOrtiz/TouchImageView

namespace HomeCatalog.Android
{
	public class PhotoFullImageView : ImageView, View.IOnTouchListener
	{
		private static readonly String DEBUG = "DEBUG";
		//
		// SuperMin and SuperMax multipliers. Determine how much the image can be
		// zoomed below or above the zoom boundaries, before animating back to the
		// min/max zoom boundary.
		//
		private static readonly float SUPER_MIN_MULTIPLIER = .75f;
		private static readonly float SUPER_MAX_MULTIPLIER = 1.25f;
		//
		// Scale of image ranges from minScale to maxScale, where minScale == 1
		// when the image is stretched to fit view.
		//
		private float normalizedScale;
		//
		// Matrix applied to image. MSCALE_X and MSCALE_Y should always be equal.
		// MtransX and MtransY are the other values used. prevMatrix is the matrix
		// saved prior to the screen rotating.
		//
		private Matrix matrix, prevMatrix;

		public enum State
		{
			NONE,
			DRAG,
			ZOOM,
			FLING,
			ANIMATE_ZOOM}

		;

		private PhotoFullImageView.State state;
		private float minScale;
		private float maxScale;
		private float superMinScale;
		private float superMaxScale;
		private float[] m;
		//private Context context;
		private Fling fling;
		//
		// Size of view and previous view size (ie before rotation)
		//
		private int viewWidth, viewHeight, prevViewWidth, prevViewHeight;
		//
		// Size of image when it is stretched to fit view. Before and After rotation.
		//
		private float matchViewWidth, matchViewHeight, prevMatchViewWidth, prevMatchViewHeight;
		//
		// After setting image, a value of true means the new image should maintain
		// the zoom of the previous image. False means it should be resized within the view.
		//
		private bool maintainZoomAfterSetImage;
		//
		// True when maintainZoomAfterSetImage has been set to true and setImage has been called.
		//
		private bool setImageCalledRecenterImage;
		private ScaleGestureDetector mScaleDetector;
		private GestureDetector mGestureDetector;

		public PhotoFullImageView (Context context) :
			base (context)
		{
			Initialize ();
		}

		public PhotoFullImageView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public PhotoFullImageView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{
			base.Clickable = true;
			mScaleDetector = new ScaleGestureDetector (Context, new ScaleListener (this));
			mGestureDetector = new GestureDetector (Context, new GestureListener (this));
			matrix = new Matrix ();
			prevMatrix = new Matrix ();
			m = new float[9];
			normalizedScale = 1;
			minScale = 1;
			maxScale = 3;
			superMinScale = SUPER_MIN_MULTIPLIER * minScale;
			superMaxScale = SUPER_MAX_MULTIPLIER * maxScale;
			maintainZoomAfterSetImage = true;
			ImageMatrix = matrix;
			SetScaleType (ScaleType.Matrix);
			state = State.NONE;
			SetOnTouchListener (this);
		}

		public override void SetImageResource (int resId)
		{
			base.SetImageResource (resId);
			setImageCalled ();
			savePreviousImageValues ();
			fitImageToView ();
		}

		public override void SetImageBitmap (Bitmap bm)
		{
			base.SetImageBitmap (bm);
			setImageCalled ();
			savePreviousImageValues ();
			fitImageToView ();
		}

		public override void SetImageDrawable (Drawable drawable)
		{
			base.SetImageDrawable (drawable);
			setImageCalled ();
			savePreviousImageValues ();
			fitImageToView ();
		}

		public override void SetImageURI (global::Android.Net.Uri uri)
		{
			base.SetImageURI (uri);
			setImageCalled ();
			savePreviousImageValues ();
			fitImageToView ();
		}

		private void setImageCalled ()
		{
			if (!maintainZoomAfterSetImage) {
				setImageCalledRecenterImage = true;
			}
		}

		/**
     * Save the current matrix and view dimensions
     * in the prevMatrix and prevView variables.
     */
		private void savePreviousImageValues ()
		{
			if (matrix != null) {
				matrix.GetValues (m);
				prevMatrix.SetValues (m);
				prevMatchViewHeight = matchViewHeight;
				prevMatchViewWidth = matchViewWidth;
				prevViewHeight = viewHeight;
				prevViewWidth = viewWidth;
			}
		}

		protected override IParcelable OnSaveInstanceState ()
		{
			Bundle bundle = new Bundle ();
			bundle.PutParcelable ("instanceState", base.OnSaveInstanceState ());
			bundle.PutFloat ("saveScale", normalizedScale);
			bundle.PutFloat ("matchViewHeight", matchViewHeight);
			bundle.PutFloat ("matchViewWidth", matchViewWidth);
			bundle.PutInt ("viewWidth", viewWidth);
			bundle.PutInt ("viewHeight", viewHeight);
			matrix.GetValues (m);
			bundle.PutFloatArray ("matrix", m);
			return bundle;
		}

		protected override void OnRestoreInstanceState (IParcelable theState)
		{
			if (theState is Bundle) {
				Bundle bundle = (Bundle)theState;
				normalizedScale = bundle.GetFloat ("saveScale");
				m = bundle.GetFloatArray ("matrix");
				prevMatrix.SetValues (m);
				prevMatchViewHeight = bundle.GetFloat ("matchViewHeight");
				prevMatchViewWidth = bundle.GetFloat ("matchViewWidth");
				prevViewHeight = bundle.GetInt ("viewHeight");
				prevViewWidth = bundle.GetInt ("viewWidth");
				base.OnRestoreInstanceState ((IParcelable)bundle.GetParcelable ("instanceState"));
				return;
			}

			base.OnRestoreInstanceState (theState);
		}

		/**
     * Get the max zoom multiplier.
     * @return max zoom multiplier.
     */
		public float getMaxZoom ()
		{
			return maxScale;
		}

		/**
     * Set the max zoom multiplier. Default value: 3.
     * @param max max zoom multiplier.
     */
		public void setMaxZoom (float max)
		{
			maxScale = max;
			superMaxScale = SUPER_MAX_MULTIPLIER * maxScale;
		}

		/**
     * Get the min zoom multiplier.
     * @return min zoom multiplier.
     */
		public float getMinZoom ()
		{
			return minScale;
		}

		/**
     * After setting image, a value of true means the new image should maintain
     * the zoom of the previous image. False means the image should be resized within
     * the view. Defaults value is true.
     * @param maintainZoom
     */
		public void MaintainZoomAfterSetImage (bool maintainZoom)
		{
			maintainZoomAfterSetImage = maintainZoom;
		}

		/**
     * Get the current zoom. This is the zoom relative to the initial
     * scale, not the original resource.
     * @return current zoom multiplier.
     */
		public float getCurrentZoom ()
		{
			return normalizedScale;
		}

		/**
     * Set the min zoom multiplier. Default value: 1.
     * @param min min zoom multiplier.
     */
		public void setMinZoom (float min)
		{
			minScale = min;
			superMinScale = SUPER_MIN_MULTIPLIER * minScale;
		}

		/**
     * For a given point on the view (ie, a touch event), returns the
     * point relative to the original drawable's coordinate system.
     * @param x
     * @param y
     * @return PointF relative to original drawable's coordinate system.
     */
		public PointF getDrawablePointFromTouchPoint (float x, float y)
		{
			return transformCoordTouchToBitmap (x, y, true);
		}

		/**
     * For a given point on the view (ie, a touch event), returns the
     * point relative to the original drawable's coordinate system.
     * @param p
     * @return PointF relative to original drawable's coordinate system.
     */
		public PointF getDrawablePointFromTouchPoint (PointF p)
		{
			return transformCoordTouchToBitmap (p.X, p.Y, true);
		}

		/**
     * Performs boundary checking and fixes the image matrix if it 
     * is out of bounds.
     */
		private void fixTrans ()
		{
			matrix.GetValues (m);
			float transX = m [Matrix.MtransX];
			float transY = m [Matrix.MtransY];

			float fixTransX = getFixTrans (transX, viewWidth, getImageWidth ());
			float fixTransY = getFixTrans (transY, viewHeight, getImageHeight ());

			if (fixTransX != 0 || fixTransY != 0) {
				matrix.PostTranslate (fixTransX, fixTransY);
			}
		}

		/**
     * When transitioning from zooming from focus to zoom from center (or vice versa)
     * the image can become unaligned within the view. This is apparent when zooming
     * quickly. When the content size is less than the view size, the content will often
     * be centered incorrectly within the view. fixScaleTrans first calls fixTrans() and 
     * then makes sure the image is centered correctly within the view.
     */
		private void fixScaleTrans ()
		{
			fixTrans ();
			matrix.GetValues (m);
			if (getImageWidth () < viewWidth) {
				m [Matrix.MtransX] = (viewWidth - getImageWidth ()) / 2;
			}

			if (getImageHeight () < viewHeight) {
				m [Matrix.MtransY] = (viewHeight - getImageHeight ()) / 2;
			}
			matrix.SetValues (m);
		}

		private float getFixTrans (float trans, float viewSize, float contentSize)
		{
			float minTrans, maxTrans;

			if (contentSize <= viewSize) {
				minTrans = 0;
				maxTrans = viewSize - contentSize;

			} else {
				minTrans = viewSize - contentSize;
				maxTrans = 0;
			}

			if (trans < minTrans)
				return -trans + minTrans;
			if (trans > maxTrans)
				return -trans + maxTrans;
			return 0;
		}

		private float getFixDragTrans (float delta, float viewSize, float contentSize)
		{
			if (contentSize <= viewSize) {
				return 0;
			}
			return delta;
		}

		private float getImageWidth ()
		{
			return matchViewWidth * normalizedScale;
		}

		private float getImageHeight ()
		{
			return matchViewHeight * normalizedScale;
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			Drawable drawable = Drawable;
			if (drawable == null || drawable.IntrinsicWidth == 0 || drawable.IntrinsicHeight == 0) {
				SetMeasuredDimension (0, 0);
				return;
			}

			int drawableWidth = drawable.IntrinsicWidth;
			int drawableHeight = drawable.IntrinsicHeight;
			int widthSize = MeasureSpec.GetSize (widthMeasureSpec);
			MeasureSpecMode widthMode = MeasureSpec.GetMode (widthMeasureSpec);
			int heightSize = MeasureSpec.GetSize (heightMeasureSpec);
			MeasureSpecMode heightMode = MeasureSpec.GetMode (heightMeasureSpec);
			viewWidth = setViewSize (widthMode, widthSize, drawableWidth);
			viewHeight = setViewSize (heightMode, heightSize, drawableHeight);

			//
			// Set view dimensions
			//
			SetMeasuredDimension (viewWidth, viewHeight);

			//
			// Fit content within view
			//
			fitImageToView ();
		}

		/**
     * If the normalizedScale is equal to 1, then the image is made to fit the screen. Otherwise,
     * it is made to fit the screen according to the dimensions of the previous image matrix. This
     * allows the image to maintain its zoom after rotation.
     */
		private void fitImageToView ()
		{
			Drawable drawable = Drawable;
			if (drawable == null || drawable.IntrinsicWidth == 0 || drawable.IntrinsicHeight == 0) {
				return;
			}
			if (matrix == null || prevMatrix == null) {
				return;
			}

			int drawableWidth = drawable.IntrinsicWidth;
			int drawableHeight = drawable.IntrinsicHeight;

			//
			// Scale image for view
			//
			float scaleX = (float)viewWidth / drawableWidth;
			float scaleY = (float)viewHeight / drawableHeight;
			float scale = Math.Min (scaleX, scaleY);

			//
			// Center the image
			//
			float redundantYSpace = viewHeight - (scale * drawableHeight);
			float redundantXSpace = viewWidth - (scale * drawableWidth);
			matchViewWidth = viewWidth - redundantXSpace;
			matchViewHeight = viewHeight - redundantYSpace;
			if (normalizedScale == 1 || setImageCalledRecenterImage) {
				//
				// Stretch and center image to fit view
				//
				matrix.SetScale (scale, scale);
				matrix.PostTranslate (redundantXSpace / 2, redundantYSpace / 2);
				normalizedScale = 1;
				setImageCalledRecenterImage = false;

			} else {
				prevMatrix.GetValues (m);

				//
				// Rescale Matrix after rotation
				//
				m [Matrix.MscaleX] = matchViewWidth / drawableWidth * normalizedScale;
				m [Matrix.MscaleY] = matchViewHeight / drawableHeight * normalizedScale;

				//
				// TransX and TransY from previous matrix
				//
				float transX = m [Matrix.MtransX];
				float transY = m [Matrix.MtransY];

				//
				// Width
				//
				float prevActualWidth = prevMatchViewWidth * normalizedScale;
				float actualWidth = getImageWidth ();
				translateMatrixAfterRotate (Matrix.MtransX, transX, prevActualWidth, actualWidth, prevViewWidth, viewWidth, drawableWidth);

				//
				// Height
				//
				float prevActualHeight = prevMatchViewHeight * normalizedScale;
				float actualHeight = getImageHeight ();
				translateMatrixAfterRotate (Matrix.MtransY, transY, prevActualHeight, actualHeight, prevViewHeight, viewHeight, drawableHeight);

				//
				// Set the matrix to the adjusted scale and translate values.
				//
				matrix.SetValues (m);
			}
			ImageMatrix = matrix;
		}

		/**
     * Set view dimensions based on layout params
     * 
     * @param mode 
     * @param size
     * @param drawableWidth
     * @return
     */
		private int setViewSize (MeasureSpecMode mode, int size, int drawableWidth)
		{
			int viewSize;

			switch (mode) {
			case MeasureSpecMode.Exactly:
				viewSize = size;
				break;

			case MeasureSpecMode.AtMost:
				viewSize = Math.Min (drawableWidth, size);
				break;

			case MeasureSpecMode.Unspecified:
				viewSize = drawableWidth;
				break;

			default:
				viewSize = size;
				break;
			}
			return viewSize;
		}

		/**
     * After rotating, the matrix needs to be translated. This function finds the area of image 
     * which was previously centered and adjusts translations so that is again the center, post-rotation.
     * 
     * @param axis Matrix.MtransX or Matrix.MtransY
     * @param trans the value of trans in that axis before the rotation
     * @param prevImageSize the width/height of the image before the rotation
     * @param imageSize width/height of the image after rotation
     * @param prevViewSize width/height of view before rotation
     * @param viewSize width/height of view after rotation
     * @param drawableSize width/height of drawable
     */
		private void translateMatrixAfterRotate (int axis, float trans, float prevImageSize, float imageSize, int prevViewSize, int viewSize, int drawableSize)
		{
			if (imageSize < viewSize) {
				//
				// The width/height of image is less than the view's width/height. Center it.
				//
				m [axis] = (viewSize - (drawableSize * m [Matrix.MscaleX])) * 0.5f;

			} else if (trans > 0) {
				//
				// The image is larger than the view, but was not before rotation. Center it.
				//
				m [axis] = -((imageSize - viewSize) * 0.5f);

			} else {
				//
				// Find the area of the image which was previously centered in the view. Determine its distance
				// from the left/top side of the view as a fraction of the entire image's width/height. Use that percentage
				// to calculate the trans in the new view width/height.
				//
				float percentage = (Math.Abs (trans) + (0.5f * prevViewSize)) / prevImageSize;
				m [axis] = -((percentage * imageSize) - (viewSize * 0.5f));
			}
		}

		private void setState (PhotoFullImageView.State state)
		{
			this.state = state;
		}

		/**
     * Gesture Listener detects a single click or long click and passes that on
     * to the view's listener.
     * @author Ortiz
     *
     */
		private class GestureListener : GestureDetector.SimpleOnGestureListener
		{
			PhotoFullImageView P;

			public GestureListener (PhotoFullImageView parent)
			{
				P = parent;
			}

			public override bool OnSingleTapConfirmed (MotionEvent e)
			{
				return P.PerformClick ();
			}

			public override void OnLongPress (MotionEvent e)
			{
				P.PerformLongClick ();
			}

			public override bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
			{
				if (P.fling != null) {
					//
					// If a previous fling is still active, it should be cancelled so that two flings
					// are not run simultaenously.
					//
					P.fling.cancelFling ();
				}
				P.fling = new Fling (P, (int)velocityX, (int)velocityY);
				P.CompatPostOnAnimation (P.fling);
				return base.OnFling (e1, e2, velocityX, velocityY);
			}

			public override bool OnDoubleTap (MotionEvent e)
			{
				bool consumed = false;
				if (P.state == PhotoFullImageView.State.NONE) {
					float targetZoom = (P.normalizedScale == P.minScale) ? P.maxScale : P.minScale;
					DoubleTapZoom doubleTap = new DoubleTapZoom (P, targetZoom, e.GetX (), e.GetY (), false);
					P.CompatPostOnAnimation (doubleTap);
					consumed = true;
				}
				return consumed;
			}
		}

		/**
     * Responsible for all touch events. Handles the heavy lifting of drag and also sends
     * touch events to Scale Detector and Gesture Detector.
     * @author Ortiz
     *
     */

		private PointF last = new PointF ();

		public bool OnTouch (View v, MotionEvent e)
		{
			mScaleDetector.OnTouchEvent (e);
			mGestureDetector.OnTouchEvent (e);
			PointF curr = new PointF (e.GetX (), e.GetY ());

			if (state == PhotoFullImageView.State.NONE ||
			     state == PhotoFullImageView.State.DRAG ||
			     state == PhotoFullImageView.State.FLING) {
				switch (e.Action) {
				case MotionEventActions.Down:
					last.Set (curr);
					if (fling != null)
						fling.cancelFling ();

					state = PhotoFullImageView.State.DRAG;
					break;

				case MotionEventActions.Move:
					if (state == PhotoFullImageView.State.DRAG) {
						float deltaX = curr.X - last.X;
						float deltaY = curr.Y - last.Y;
						float fixTransX = getFixDragTrans (deltaX, viewWidth, getImageWidth ());
						float fixTransY = getFixDragTrans (deltaY, viewHeight, getImageHeight ());
						matrix.PostTranslate (fixTransX, fixTransY);
						fixTrans ();
						last.Set (curr.X, curr.Y);
					}
					break;

				case MotionEventActions.Up:
				case MotionEventActions.PointerUp:
					setState (PhotoFullImageView.State.NONE);
					break;
				}
			}

			ImageMatrix = matrix;
			//
			// indicate event was handled
			//
			return true;
		}

		/**
     * ScaleListener detects user two finger scaling and scales image.
     * @author Ortiz
     *
     */
		private class ScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
		{
			PhotoFullImageView P;

			public ScaleListener (PhotoFullImageView parent)
			{
				P = parent;
			}

			public override bool OnScaleBegin (ScaleGestureDetector detector)
			{
				P.state = PhotoFullImageView.State.ZOOM;
				return true;
			}

			public override bool OnScale (ScaleGestureDetector detector)
			{
				P.scaleImage (detector.ScaleFactor, detector.FocusX, detector.FocusY, true);
				return true;
			}

			public override void OnScaleEnd (ScaleGestureDetector detector)
			{
				base.OnScaleEnd (detector);
				P.state = PhotoFullImageView.State.NONE;
				bool animateToZoomBoundary = false;
				float targetZoom = P.normalizedScale;
				if (P.normalizedScale > P.maxScale) {
					targetZoom = P.maxScale;
					animateToZoomBoundary = true;

				} else if (P.normalizedScale < P.minScale) {
					targetZoom = P.minScale;
					animateToZoomBoundary = true;
				}

				if (animateToZoomBoundary) {
					DoubleTapZoom doubleTap = new DoubleTapZoom (P, targetZoom, P.viewWidth / 2, P.viewHeight / 2, true);
					P.CompatPostOnAnimation (doubleTap);
				}
			}
		}

		private void scaleImage (float deltaScale, float focusX, float focusY, bool stretchImageToSuper)
		{

			float lowerScale, upperScale;
			if (stretchImageToSuper) {
				lowerScale = superMinScale;
				upperScale = superMaxScale;

			} else {
				lowerScale = minScale;
				upperScale = maxScale;
			}

			float origScale = normalizedScale;
			normalizedScale *= deltaScale;
			if (normalizedScale > upperScale) {
				normalizedScale = upperScale;
				deltaScale = upperScale / origScale;
			} else if (normalizedScale < lowerScale) {
				normalizedScale = lowerScale;
				deltaScale = lowerScale / origScale;
			}

			matrix.PostScale (deltaScale, deltaScale, focusX, focusY);
			fixScaleTrans ();
		}

		/**
     * DoubleTapZoom calls a series of runnables which apply
     * an animated zoom in/out graphic to the image.
     * @author Ortiz
     *
     */
		//ZACH NOTE: Trying to find out how to turn the Runnable into a delegate. Just function with initializers and an operation.
		protected class DoubleTapZoom : HomeCatalog.Core.IRunnable
		{
			private long startTime;
			private static readonly float ZOOM_TIME = 500;
			private float startZoom, targetZoom;
			private float bitmapX, bitmapY;
			private bool stretchImageToSuper;
			private AccelerateDecelerateInterpolator interpolator = new AccelerateDecelerateInterpolator ();
			private PointF startTouch;
			private PointF endTouch;
			PhotoFullImageView P;

			public DoubleTapZoom (PhotoFullImageView parent, float targetZoom, float focusX, float focusY, bool stretchImageToSuper)
			{
				P = parent;
				P.state = PhotoFullImageView.State.ANIMATE_ZOOM;
				startTime = currentMilliseconds ();
				this.startZoom = P.normalizedScale;
				this.targetZoom = targetZoom;
				this.stretchImageToSuper = stretchImageToSuper;
				PointF bitmapPoint = P.transformCoordTouchToBitmap (focusX, focusY, false);
				this.bitmapX = bitmapPoint.X;
				this.bitmapY = bitmapPoint.Y;

				//
				// Used for translating image during scaling
				//
				startTouch = P.transformCoordBitmapToTouch (bitmapX, bitmapY);
				endTouch = new PointF (P.viewWidth / 2, P.viewHeight / 2);
			}

			public void Run ()
			{
				float t = interpolate ();
				float deltaScale = calculateDeltaScale (t);
				P.scaleImage (deltaScale, bitmapX, bitmapY, stretchImageToSuper);
				translateImageToCenterTouchPosition (t);
				P.fixScaleTrans ();
				P.ImageMatrix = P.matrix;

				if (t < 1f) {
					//
					// We haven't finished zooming
					//
					P.CompatPostOnAnimation (this);

				} else {
					//
					// Finished zooming
					//
					P.state = PhotoFullImageView.State.NONE;
				}
			}

			/**
		 * Interpolate between where the image should start and end in order to translate
		 * the image so that the point that is touched is what ends up centered at the end
		 * of the zoom.
		 * @param t
		 */
			private void translateImageToCenterTouchPosition (float t)
			{
				float targetX = startTouch.X + t * (endTouch.X - startTouch.X);
				float targetY = startTouch.Y + t * (endTouch.Y - startTouch.Y);
				PointF curr = P.transformCoordBitmapToTouch (bitmapX, bitmapY);
				P.matrix.PostTranslate (targetX - curr.X, targetY - curr.Y);
			}

			/**
		 * Use interpolator to get t
		 * @return
		 */
			private float interpolate ()
			{
				long currTime = currentMilliseconds ();
				float elapsed = (currTime - startTime) / ZOOM_TIME;
				elapsed = Math.Min (1f, elapsed);
				return interpolator.GetInterpolation (elapsed);
			}

			/**
		 * Interpolate the current targeted zoom and get the delta
		 * from the current zoom.
		 * @param t
		 * @return
		 */
			private float calculateDeltaScale (float t)
			{
				float zoom = startZoom + t * (targetZoom - startZoom);
				return zoom / P.normalizedScale;
			}
		}

		/**
     * This function will transform the coordinates in the touch event to the coordinate 
     * system of the drawable that the imageview contain
     * @param x x-coordinate of touch event
     * @param y y-coordinate of touch event
     * @param clipToBitmap Touch event may occur within view, but outside image content. True, to clip return value
     * 			to the bounds of the bitmap size.
     * @return Coordinates of the point touched, in the coordinate system of the original drawable.
     */
		private PointF transformCoordTouchToBitmap (float x, float y, bool clipToBitmap)
		{
			matrix.GetValues (m);
			float origW = Drawable.IntrinsicWidth;
			float origH = Drawable.IntrinsicHeight;
			float transX = m [Matrix.MtransX];
			float transY = m [Matrix.MtransY];
			float finalX = ((x - transX) * origW) / getImageWidth ();
			float finalY = ((y - transY) * origH) / getImageHeight ();

			if (clipToBitmap) {
				finalX = Math.Min (Math.Max (x, 0), origW);
				finalY = Math.Min (Math.Max (y, 0), origH);
			}

			return new PointF (finalX, finalY);
		}

		/**
     * Inverse of transformCoordTouchToBitmap. This function will transform the coordinates in the
     * drawable's coordinate system to the view's coordinate system.
     * @param bx x-coordinate in original bitmap coordinate system
     * @param by y-coordinate in original bitmap coordinate system
     * @return Coordinates of the point in the view's coordinate system.
     */
		private PointF transformCoordBitmapToTouch (float bx, float by)
		{
			matrix.GetValues (m);        
			float origW = Drawable.IntrinsicWidth;
			float origH = Drawable.IntrinsicHeight;
			float px = bx / origW;
			float py = by / origH;
			float finalX = m [Matrix.MtransX] + getImageWidth () * px;
			float finalY = m [Matrix.MtransY] + getImageHeight () * py;
			return new PointF (finalX, finalY);
		}

		/**
     * Fling launches sequential runnables which apply
     * the fling graphic to the image. The values for the translation
     * are interpolated by the Scroller.
     * @author Ortiz
     *
     */
		protected class Fling : HomeCatalog.Core.IRunnable
		{
			Scroller scroller;
			int currX, currY;
			PhotoFullImageView P;

			public Fling (PhotoFullImageView parent, int velocityX, int velocityY)
			{
				P = parent;
				P.state = PhotoFullImageView.State.FLING;
				scroller = new Scroller (P.Context);
				P.matrix.GetValues (P.m);

				int startX = (int)P.m [Matrix.MtransX];
				int startY = (int)P.m [Matrix.MtransY];
				int minX, maxX, minY, maxY;

				if (P.getImageWidth () > P.viewWidth) {
					minX = P.viewWidth - (int)P.getImageWidth ();
					maxX = 0;

				} else {
					minX = maxX = startX;
				}

				if (P.getImageHeight () > P.viewHeight) {
					minY = P.viewHeight - (int)P.getImageHeight ();
					maxY = 0;

				} else {
					minY = maxY = startY;
				}

				scroller.Fling (startX, startY, (int)velocityX, (int)velocityY, minX,
					maxX, minY, maxY);
				currX = startX;
				currY = startY;
			}

			public void cancelFling ()
			{
				if (scroller != null) {
					P.state = PhotoFullImageView.State.NONE;
					scroller.ForceFinished (true);
				}
			}

			public void Run ()
			{
				if (scroller.IsFinished) {
					scroller = null;
					return;
				}

				if (scroller.ComputeScrollOffset ()) {
					int newX = scroller.CurrX;
					int newY = scroller.CurrY;
					int transX = newX - currX;
					int transY = newY - currY;
					currX = newX;
					currY = newY;
					P.matrix.PostTranslate (transX, transY);
					P.fixTrans ();
					P.ImageMatrix = P.matrix;
					P.CompatPostOnAnimation (this);
				}
			}
		}
		//@TargetApi(Build.VERSION_CODES.JELLY_BEAN)
		private void CompatPostOnAnimation (HomeCatalog.Core.IRunnable runnable)
		{
//				if (VERSION.SDK_INT >= VERSION_CODES.JELLY_BEAN) {
//					postOnAnimation(runnable);
//
//				} else {

			PostDelayed (runnable.Run, 1000 / 60);
//				}
		}

		private void printMatrixInfo ()
		{
			matrix.GetValues (m);
			Console.WriteLine (DEBUG, "Scale: " + m [Matrix.MscaleX] + " TransX: " + m [Matrix.MtransX] + " TransY: " + m [Matrix.MtransY]);
		}

		private static long currentMilliseconds () {
			return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		}
	}
}

