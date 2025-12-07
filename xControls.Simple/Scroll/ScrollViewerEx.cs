using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Xml.Linq;

namespace xControl.Simple.Scroll
{
    /// <summary>
    /// 自定义ScrollViewer
    /// 暴露scrollbar，并添加一些动画效果
    /// </summary>
    [TemplatePart(Name = "VerticalScrollBarPanel", Type = typeof(UIElement))]
    [TemplatePart(Name = "HorizontalScrollBarPanel", Type = typeof(UIElement))]
    [TemplatePart(Name = "VerticalTranslate", Type = typeof(TranslateTransform))]
    [TemplatePart(Name = "HorizontalTranslate", Type = typeof(TranslateTransform))]
    public sealed class ScrollViewerEx : ScrollViewer
    {
        #region Properties

        #region Animation

        #region Aim

        private Grid _vScrollPanel;

        private Grid _hScrollPanel;

        private ScrollBar _vScrollBar;

        private ScrollBar _hScrollBar;

        private TranslateTransform _verticalTranslate;

        private TranslateTransform _horizontalTranslate;
        #endregion

        #region Transform

        private DoubleAnimationUsingKeyFrames _fadeinanimation;

        private SplineDoubleKeyFrame _keyin1;

        private DoubleAnimationUsingKeyFrames _fadeoutanimation;

        private SplineDoubleKeyFrame _keyout1;

        private DoubleAnimationUsingKeyFrames _hideanimation;

        private SplineDoubleKeyFrame _keyhide1;

        private DoubleAnimationUsingKeyFrames _showanimation;

        private SplineDoubleKeyFrame _keyshow1;

        #endregion

        #endregion

        /// <summary>
        /// 获得水平滚动条
        /// </summary>
        public ScrollBarEx HorizontalScrollBar { get; set; }

        /// <summary>
        /// 获得竖直滚动条
        /// </summary>
        public ScrollBarEx VerticalScrollBar { get; set; }

        /// <summary>
        /// 滚动条布局
        /// </summary>
        public ScrollbarLayoutMode ScrollbarLayout
        {
            get { return (ScrollbarLayoutMode)GetValue(ScrollbarLayoutProperty); }
            set { SetValue(ScrollbarLayoutProperty, value); }
        }
        public static readonly DependencyProperty ScrollbarLayoutProperty =
            DependencyProperty.Register("ScrollbarLayout", typeof(ScrollbarLayoutMode),
                typeof(ScrollViewerEx), new PropertyMetadata(ScrollbarLayoutMode.Over, OnScrollbarLayoutChanged));
        private static void OnScrollbarLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((ScrollbarLayoutMode)e.NewValue == ScrollbarLayoutMode.Dock)
            {
                ((ScrollViewerEx)d).DisposeMiniable();
                ((ScrollViewerEx)d).DisposeAutoHide();
            }
            else
            {
                if (((ScrollViewerEx)d).AutoHideScrollbar)
                    ((ScrollViewerEx)d).InitAutoHide();
                if (((ScrollViewerEx)d).Miniable)
                    ((ScrollViewerEx)d).InitMiniable();
            }
        }

        /// <summary>
        /// 是否自动隐藏滚动条
        /// 仅在滚动条布局为Over时有效
        /// </summary>
        public bool AutoHideScrollbar
        {
            get { return (bool)GetValue(AutoHideScrollbarProperty); }
            set { SetValue(AutoHideScrollbarProperty, value); }
        }
        public static readonly DependencyProperty AutoHideScrollbarProperty =
            DependencyProperty.Register("AutoHideScrollbar", typeof(bool),
                typeof(ScrollViewerEx), new PropertyMetadata(true, OnAutoHideScrollbarChanged));
        private static void OnAutoHideScrollbarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                ((ScrollViewerEx)d).InitAutoHide();
            else
                ((ScrollViewerEx)d).DisposeAutoHide();
        }

        /// <summary>
        /// 是否自动收缩
        /// 仅在滚动条布局为Over时有效
        /// </summary>
        public bool Miniable
        {
            get { return (bool)GetValue(MiniableProperty); }
            set { SetValue(MiniableProperty, value); }
        }
        public static readonly DependencyProperty MiniableProperty =
            DependencyProperty.Register("Miniable", typeof(bool),
                typeof(ScrollViewerEx), new PropertyMetadata(true, OnMiniableChanged));
        private static void OnMiniableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                ((ScrollViewerEx)d).InitMiniable();
            else
                ((ScrollViewerEx)d).DisposeMiniable();
        }

        /// <summary>
        /// 滚动条的宽度
        /// </summary>
        public double ScrollbarThickness
        {
            get { return (double)GetValue(ScrollbarThicknessProperty); }
            set { SetValue(ScrollbarThicknessProperty, value); }
        }
        public static readonly DependencyProperty ScrollbarThicknessProperty =
            DependencyProperty.Register("ScrollbarThickness", typeof(double),
                typeof(ScrollViewerEx), new PropertyMetadata(24.0, OnScrollbarThicknessChanged));
        private static void OnScrollbarThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((ScrollViewerEx)d)._keyin1 != null)
                ((ScrollViewerEx)d)._keyin1.Value = (double)e.NewValue - ((ScrollViewerEx)d).ScrollbarMiniThickness;
        }

        /// <summary>
        /// 滚动条收缩起来的宽度
        /// </summary>
        public double ScrollbarMiniThickness
        {
            get { return (double)GetValue(ScrollbarMiniThicknessProperty); }
            set { SetValue(ScrollbarMiniThicknessProperty, value); }
        }
        public static readonly DependencyProperty ScrollbarMiniThicknessProperty =
            DependencyProperty.Register("ScrollbarMiniThickness", typeof(double),
                typeof(ScrollViewerEx), new PropertyMetadata(2.0, OnScrollbarMiniThicknessChanged));
        private static void OnScrollbarMiniThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((ScrollViewerEx)d)._keyin1 != null)
                ((ScrollViewerEx)d)._keyin1.Value = ((ScrollViewerEx)d).ScrollbarThickness - (double)e.NewValue;
        }

        /// <summary>
        /// 暴露出竖直滚动条的样式
        /// </summary>
        public Style VerticalScrollBarStyle
        {
            get { return (Style)GetValue(VerticalScrollBarStyleProperty); }
            set { SetValue(VerticalScrollBarStyleProperty, value); }
        }
        public static readonly DependencyProperty VerticalScrollBarStyleProperty =
            DependencyProperty.Register("VerticalScrollBarStyle", typeof(Style),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        /// <summary>
        /// 暴露出水平滚动条的样式
        /// </summary>
        public Style HorizontalScrollBarStyle
        {
            get { return (Style)GetValue(HorizontalScrollBarStyleProperty); }
            set { SetValue(HorizontalScrollBarStyleProperty, value); }
        }
        public static readonly DependencyProperty HorizontalScrollBarStyleProperty =
            DependencyProperty.Register("HorizontalScrollBarStyle", typeof(Style),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        /// <summary>
        /// 暴露出竖直滚动条滑块的样式
        /// </summary>
        public Style ThumbVertical
        {
            get { return (Style)GetValue(ThumbVerticalProperty); }
            set { SetValue(ThumbVerticalProperty, value); }
        }
        public static readonly DependencyProperty ThumbVerticalProperty =
            DependencyProperty.Register("ThumbVertical", typeof(Style),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        /// <summary>
        /// 暴露出水平滚动条滑块的样式
        /// </summary>
        public Style ThumbHorizontal
        {
            get { return (Style)GetValue(ThumbHorizontalProperty); }
            set { SetValue(ThumbHorizontalProperty, value); }
        }
        public static readonly DependencyProperty ThumbHorizontalProperty =
            DependencyProperty.Register("ThumbHorizontal", typeof(Style),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        /// <summary>
        /// 滑块颜色控制
        /// </summary>
        #region ThumbColorControl
        public Brush ThumbFill
        {
            get { return (Brush)GetValue(ThumbFillProperty); }
            set { SetValue(ThumbFillProperty, value); }
        }
        public static readonly DependencyProperty ThumbFillProperty =
            DependencyProperty.Register("ThumbFill", typeof(Brush),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        public Brush ThumbStroke
        {
            get { return (Brush)GetValue(ThumbStrokeProperty); }
            set { SetValue(ThumbStrokeProperty, value); }
        }
        public static readonly DependencyProperty ThumbStrokeProperty =
            DependencyProperty.Register("ThumbStroke", typeof(Brush),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        public Brush ThumbFill_O
        {
            get { return (Brush)GetValue(ThumbFill_OProperty); }
            set { SetValue(ThumbFill_OProperty, value); }
        }
        public static readonly DependencyProperty ThumbFill_OProperty =
            DependencyProperty.Register("ThumbFill_O", typeof(Brush),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        public Brush ThumbStroke_O
        {
            get { return (Brush)GetValue(ThumbStroke_OProperty); }
            set { SetValue(ThumbStroke_OProperty, value); }
        }
        public static readonly DependencyProperty ThumbStroke_OProperty =
            DependencyProperty.Register("ThumbStroke_O", typeof(Brush),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        public Brush ThumbFill_P
        {
            get { return (Brush)GetValue(ThumbFill_PProperty); }
            set { SetValue(ThumbFill_PProperty, value); }
        }
        public static readonly DependencyProperty ThumbFill_PProperty =
            DependencyProperty.Register("ThumbFill_P", typeof(Brush),
                typeof(ScrollViewerEx), new PropertyMetadata(null));

        public Brush ThumbStroke_P
        {
            get { return (Brush)GetValue(ThumbStroke_PProperty); }
            set { SetValue(ThumbStroke_PProperty, value); }
        }
        public static readonly DependencyProperty ThumbStroke_PProperty =
            DependencyProperty.Register("ThumbStroke_P", typeof(Brush),
                typeof(ScrollViewerEx), new PropertyMetadata(null));
        #endregion

        #endregion

        #region Methods

        #region Animation
        public void ScrollBarMini()
        {
            _verticalTranslate.BeginAnimation(TranslateTransform.XProperty, _fadeinanimation);
            _horizontalTranslate.BeginAnimation(TranslateTransform.YProperty, _fadeinanimation);
        }

        public void ScrollBarNormal()
        {
            _verticalTranslate.BeginAnimation(TranslateTransform.XProperty, _fadeoutanimation);
            _horizontalTranslate.BeginAnimation(TranslateTransform.YProperty, _fadeoutanimation);
        }

        public void HideScrollbar()
        {
            _vScrollBar.BeginAnimation(ScrollBar.OpacityProperty, _hideanimation);
            _hScrollBar.BeginAnimation(ScrollBar.OpacityProperty, _hideanimation);
        }

        public void ShowScrollbar()
        {
            _vScrollBar.BeginAnimation(ScrollBar.OpacityProperty, _showanimation);
            _hScrollBar.BeginAnimation(ScrollBar.OpacityProperty, _showanimation);
        }

        private void InitAutoHide()
        {
            _vScrollBar = GetTemplateChild("PART_VerticalScrollBar") as ScrollBar;
            _hScrollBar = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;
            MouseEnter += ScrollViewerEx_MouseEnter;
            MouseLeave += ScrollViewerEx_MouseLeave;

            _hideanimation = new DoubleAnimationUsingKeyFrames();
            _keyhide1 = new SplineDoubleKeyFrame
            {
                KeySpline = new KeySpline(0, 0.4, 0.6, 1),
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(320)),
                Value = 0,
            };
            _hideanimation.KeyFrames.Add(_keyhide1);

            _showanimation = new DoubleAnimationUsingKeyFrames();
            _keyshow1 = new SplineDoubleKeyFrame
            {
                KeySpline = new KeySpline(0, 0.4, 0.6, 1),
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(320)),
                Value = 1,
            };
            _showanimation.KeyFrames.Add(_keyshow1);

            HideScrollbar();
        }

        private void DisposeAutoHide()
        {
            if (_vScrollBar != null)
                ShowScrollbar();

            MouseEnter -= ScrollViewerEx_MouseEnter;
            MouseLeave -= ScrollViewerEx_MouseLeave;
            _vScrollBar = null;
            _hScrollBar = null;
        }

        private void InitMiniable()
        {
            _vScrollPanel = GetTemplateChild("VerticalScrollBarPanel") as Grid;
            _vScrollPanel.MouseEnter += _ScrollPanel_MouseEnter;
            _vScrollPanel.MouseLeave += _ScrollPanel_MouseLeave;
            _hScrollPanel = GetTemplateChild("HorizontalScrollBarPanel") as Grid;
            _hScrollPanel.MouseEnter += _ScrollPanel_MouseEnter;
            _hScrollPanel.MouseLeave += _ScrollPanel_MouseLeave;
            _verticalTranslate = GetTemplateChild("VerticalTranslate") as TranslateTransform;
            _horizontalTranslate = GetTemplateChild("HorizontalTranslate") as TranslateTransform;

            _fadeinanimation = new DoubleAnimationUsingKeyFrames
            {
                BeginTime = TimeSpan.FromMilliseconds(500),
            };
            _keyin1 = new SplineDoubleKeyFrame
            {
                KeySpline = new KeySpline(0, 0.4, 0.6, 1),
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(320)),
                Value = ScrollbarThickness - ScrollbarMiniThickness,
            };
            _fadeinanimation.KeyFrames.Add(_keyin1);

            _fadeoutanimation = new DoubleAnimationUsingKeyFrames();
            _keyout1 = new SplineDoubleKeyFrame
            {
                KeySpline = new KeySpline(0, 0.4, 0.6, 1),
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(320)),
                Value = 0,
            };
            _fadeoutanimation.KeyFrames.Add(_keyout1);

            ScrollBarMini();
        }

        private void DisposeMiniable()
        {
            if (_vScrollPanel != null)
            {
                ScrollBarNormal();

                _vScrollPanel.MouseEnter -= _ScrollPanel_MouseEnter;
                _vScrollPanel.MouseLeave -= _ScrollPanel_MouseLeave;
                _hScrollPanel.MouseEnter -= _ScrollPanel_MouseEnter;
                _hScrollPanel.MouseLeave -= _ScrollPanel_MouseLeave;
                _vScrollPanel = null;
                _hScrollPanel = null;
                _verticalTranslate = null;
                _horizontalTranslate = null;
            }
        }
        #endregion

        private void ScrollViewerEx_MouseLeave(object sender, MouseEventArgs e)
        {
            HideScrollbar();
        }

        private void ScrollViewerEx_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowScrollbar();
        }

        private void _ScrollPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            ScrollBarMini();
        }

        private void _ScrollPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            ScrollBarNormal();
        }


        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            if (ScrollbarLayout == ScrollbarLayoutMode.Over)
            {
                if (AutoHideScrollbar)
                    InitAutoHide();
                if (Miniable)
                    InitMiniable();
            }

            HorizontalScrollBar = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBarEx;
            VerticalScrollBar = GetTemplateChild("PART_VerticalScrollBar") as ScrollBarEx;

            base.OnApplyTemplate();
        }
        #endregion

        #region Constructors
        public ScrollViewerEx() : base()
        {

        }

        static ScrollViewerEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewerEx), new FrameworkPropertyMetadata(typeof(ScrollViewerEx)));
        }
        #endregion

        /// <summary>
        /// 滚动条的布局
        /// </summary>
        public enum ScrollbarLayoutMode
        {
            /// <summary>
            /// 悬浮在滚动区域上
            /// </summary>
            Over,
            /// <summary>
            /// 在滚动区域边停靠
            /// </summary>
            Dock,
        }
    }
}
