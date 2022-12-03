﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace xControl.Simple.Primitives
{

    public class ThumbEx : Thumb
    {

        #region Properties
        /// <summary>
        /// 图标模式
        /// </summary>
        public ThumbIconType IconMode
        {
            get { return (ThumbIconType)GetValue(IconModeProperty); }
            set { SetValue(IconModeProperty, value); }
        }
        public static readonly DependencyProperty IconModeProperty =
            DependencyProperty.Register("IconMode", typeof(ThumbIconType),
                typeof(ThumbEx), new FrameworkPropertyMetadata(ThumbIconType.Rect, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 在图标模式设置为FontIcon时有效，
        /// 为Icon使用的图标字族
        /// </summary>
        public FontFamily IconFont
        {
            get { return (FontFamily)GetValue(IconFontProperty); }
            set { SetValue(IconFontProperty, value); }
        }
        public static readonly DependencyProperty IconFontProperty =
            DependencyProperty.Register("IconFont", typeof(FontFamily),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new FontFamily(), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 在图标模式设置为FontIcon、Path时有效
        /// Path 模式时 默认画布大小为 20 * 20 在Scrollbar下 此区域将被拉伸
        /// </summary>
        public String ThumbIcon
        {
            get { return (String)GetValue(ThumbIconProperty); }
            set { SetValue(ThumbIconProperty, value); }
        }
        public static readonly DependencyProperty ThumbIconProperty =
            DependencyProperty.Register("ThumbIcon", typeof(String),
                typeof(ThumbEx), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 使用路径模式图标时路径的左端或上端形状 默认画布大小为 20 * 20 
        /// </summary>
        public string PathCapLoT
        {
            get { return (string)GetValue(PathCapLoTProperty); }
            set { SetValue(PathCapLoTProperty, value); }
        }
        public static readonly DependencyProperty PathCapLoTProperty =
            DependencyProperty.Register("PathCapLoT", typeof(string),
                typeof(ThumbEx), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 使用路径模式图标时路径的右端或下端形状 默认画布大小为 20 * 20 
        /// </summary>
        public string PathCapRoB
        {
            get { return (string)GetValue(PathCapRoBProperty); }
            set { SetValue(PathCapRoBProperty, value); }
        }
        public static readonly DependencyProperty PathCapRoBProperty =
            DependencyProperty.Register("PathCapRoB", typeof(string),
                typeof(ThumbEx), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 方向，默认竖直
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation),
                typeof(ThumbEx), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// 图标的描边
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double),
                typeof(ThumbEx), new PropertyMetadata(1.0));

        #region Icon
        public Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility),
                typeof(ThumbEx), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));

        #region Alignment
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.Inherits));

        public HorizontalAlignment IconHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
            set { SetValue(IconHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconHorizontalAlignmentProperty =
            DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment),
                typeof(ThumbEx), new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public VerticalAlignment IconVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconVerticalAlignmentProperty =
            DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment),
                typeof(ThumbEx), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        public TextAlignment IconTextAlignment
        {
            get { return (TextAlignment)GetValue(IconTextAlignmentProperty); }
            set { SetValue(IconTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconTextAlignmentProperty =
            DependencyProperty.Register("IconTextAlignment", typeof(TextAlignment),
                typeof(ThumbEx), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Color

        public Brush IconFgNormal
        {
            get { return (Brush)GetValue(IconFgNormalProperty); }
            set { SetValue(IconFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconFgNormalProperty =
            DependencyProperty.Register("IconFgNormal", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgOver
        {
            get { return (Brush)GetValue(IconFgOverProperty); }
            set { SetValue(IconFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconFgOverProperty =
            DependencyProperty.Register("IconFgOver", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgPressed
        {
            get { return (Brush)GetValue(IconFgPressedProperty); }
            set { SetValue(IconFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconFgPressedProperty =
            DependencyProperty.Register("IconFgPressed", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconFgDisabled
        {
            get { return (Brush)GetValue(IconFgDisabledProperty); }
            set { SetValue(IconFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconFgDisabledProperty =
            DependencyProperty.Register("IconFgDisabled", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgNormal
        {
            get { return (Brush)GetValue(IconBgNormalProperty); }
            set { SetValue(IconBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconBgNormalProperty =
            DependencyProperty.Register("IconBgNormal", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgOver
        {
            get { return (Brush)GetValue(IconBgOverProperty); }
            set { SetValue(IconBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconBgOverProperty =
            DependencyProperty.Register("IconBgOver", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgPressed
        {
            get { return (Brush)GetValue(IconBgPressedProperty); }
            set { SetValue(IconBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconBgPressedProperty =
            DependencyProperty.Register("IconBgPressed", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconBgDisabled
        {
            get { return (Brush)GetValue(IconBgDisabledProperty); }
            set { SetValue(IconBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconBgDisabledProperty =
            DependencyProperty.Register("IconBgDisabled", typeof(Brush),
                typeof(ThumbEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)), FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        public Style ToolTipTextStyle
        {
            get { return (Style)GetValue(ToolTipTextStyleProperty); }
            set { SetValue(ToolTipTextStyleProperty, value); }
        }
        public static readonly DependencyProperty ToolTipTextStyleProperty =
            DependencyProperty.Register("ToolTipTextStyle", typeof(Style),
                typeof(ThumbEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public Style ToolTipStyle
        {
            get { return (Style)GetValue(ToolTipStyleProperty); }
            set { SetValue(ToolTipStyleProperty, value); }
        }
        public static readonly DependencyProperty ToolTipStyleProperty =
            DependencyProperty.Register("ToolTipStyle", typeof(Style),
                typeof(ThumbEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public Visibility ToolTipVisibility
        {
            get { return (Visibility)GetValue(ToolTipVisibilityProperty); }
            set { SetValue(ToolTipVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisibilityProperty =
            DependencyProperty.Register("ToolTipVisibility", typeof(Visibility),
                typeof(ThumbEx), new PropertyMetadata(Visibility.Collapsed));

        public string ToolTipString
        {
            get { return (string)GetValue(ToolTipStringProperty); }
            set { SetValue(ToolTipStringProperty, value); }
        }
        public static readonly DependencyProperty ToolTipStringProperty =
            DependencyProperty.Register("ToolTipString", typeof(string),
                typeof(ThumbEx), new FrameworkPropertyMetadata("This is a ToolTip", FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Methods

        #endregion

        #region Constructors
        static ThumbEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThumbEx), new FrameworkPropertyMetadata(typeof(ThumbEx), FrameworkPropertyMetadataOptions.Inherits));
        }

        #endregion

        /// <summary>
        /// 滑动游标的图表类型
        /// </summary>
        public enum ThumbIconType
        {
            /// <summary>
            /// 使用图标字体
            /// </summary>
            FontIcon,
            /// <summary>
            /// 使用路径图标
            /// </summary>
            Path,
            /// <summary>
            /// 矩形
            /// </summary>
            Rect,
            /// <summary>
            /// 圆形
            /// </summary>
            Ellipse,
            /// <summary>
            /// 只有MainPath
            /// </summary>
            PathMain,
        }
    }
}
