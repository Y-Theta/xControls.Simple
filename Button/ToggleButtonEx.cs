﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace xControl.Simple.Button
{
    [DefaultEvent("Checked")]
    public class ToggleButtonEx : ButtonEx
    {

        #region Properties

        #region Icon
        public new string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public string IconSelect
        {
            get { return (string)GetValue(IconSelectProperty); }
            set { SetValue(IconSelectProperty, value); }
        }
        public static readonly DependencyProperty IconSelectProperty =
            DependencyProperty.Register("IconSelect", typeof(string),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        #region Color
        public Brush IconSelectFgNormal
        {
            get { return (Brush)GetValue(IconSelectFgNormalProperty); }
            set { SetValue(IconSelectFgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgNormalProperty =
            DependencyProperty.Register("IconSelectFgNormal", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgOver
        {
            get { return (Brush)GetValue(IconSelectFgOverProperty); }
            set { SetValue(IconSelectFgOverProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgOverProperty =
            DependencyProperty.Register("IconSelectFgOver", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgPressed
        {
            get { return (Brush)GetValue(IconSelectFgPressedProperty); }
            set { SetValue(IconSelectFgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgPressedProperty =
            DependencyProperty.Register("IconSelectFgPressed", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectFgDisabled
        {
            get { return (Brush)GetValue(IconSelectFgDisabledProperty); }
            set { SetValue(IconSelectFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconSelectFgDisabledProperty =
            DependencyProperty.Register("IconSelectFgDisabled", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgNormal
        {
            get { return (Brush)GetValue(IconSelectBgNormalProperty); }
            set { SetValue(IconSelectBgNormalProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgNormalProperty =
            DependencyProperty.Register("IconSelectBgNormal", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgOver
        {
            get { return (Brush)GetValue(IconSelectBgOverProperty); }
            set { SetValue(IconSelectBgOverProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgOverProperty =
            DependencyProperty.Register("IconSelectBgOver", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgPressed
        {
            get { return (Brush)GetValue(IconSelectBgPressedProperty); }
            set { SetValue(IconSelectBgPressedProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgPressedProperty =
            DependencyProperty.Register("IconSelectBgPressed", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 16, 16, 16)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush IconSelectBgDisabled
        {
            get { return (Brush)GetValue(IconSelectBgDisabledProperty); }
            set { SetValue(IconSelectBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty IconSelectBgDisabledProperty =
            DependencyProperty.Register("IconSelectBgDisabled", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #endregion

        #region Label

        public string LabelSelect
        {
            get { return (string)GetValue(LabelSelectProperty); }
            set { SetValue(LabelSelectProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectProperty =
            DependencyProperty.Register("LabelSelect", typeof(string),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        #region Color
        public Brush LabelSelectFgNormal
        {
            get { return (Brush)GetValue(LabelSelectFgNormalProperty); }
            set { SetValue(LabelSelectFgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgNormalProperty =
            DependencyProperty.Register("LabelSelectFgNormal", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgOver
        {
            get { return (Brush)GetValue(LabelSelectFgOverProperty); }
            set { SetValue(LabelSelectFgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgOverProperty =
            DependencyProperty.Register("LabelSelectFgOver", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgPressed
        {
            get { return (Brush)GetValue(LabelSelectFgPressedProperty); }
            set { SetValue(LabelSelectFgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgPressedProperty =
            DependencyProperty.Register("LabelSelectFgPressed", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 180, 180, 180)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectFgDisabled
        {
            get { return (Brush)GetValue(LabelSelectFgDisabledProperty); }
            set { SetValue(LabelSelectFgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectFgDisabledProperty =
            DependencyProperty.Register("LabelSelectFgDisabled", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgNormal
        {
            get { return (Brush)GetValue(LabelSelectBgNormalProperty); }
            set { SetValue(LabelSelectBgNormalProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgNormalProperty =
            DependencyProperty.Register("LabelSelectBgNormal", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgOver
        {
            get { return (Brush)GetValue(LabelSelectBgOverProperty); }
            set { SetValue(LabelSelectBgOverProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgOverProperty =
            DependencyProperty.Register("LabelSelectBgOver", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgPressed
        {
            get { return (Brush)GetValue(LabelSelectBgPressedProperty); }
            set { SetValue(LabelSelectBgPressedProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgPressedProperty =
            DependencyProperty.Register("LabelSelectBgPressed", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 16, 16, 16)),
                    FrameworkPropertyMetadataOptions.Inherits));

        public Brush LabelSelectBgDisabled
        {
            get { return (Brush)GetValue(LabelSelectBgDisabledProperty); }
            set { SetValue(LabelSelectBgDisabledProperty, value); }
        }
        public static readonly DependencyProperty LabelSelectBgDisabledProperty =
            DependencyProperty.Register("LabelSelectBgDisabled", typeof(Brush),
                typeof(ToggleButtonEx), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 80, 80, 80)),
                    FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #endregion

        [Category("Appearance")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked
        {
            get
            {
                object value = GetValue(IsCheckedProperty);
                if (value == null)
                    return new bool?();
                else
                    return new bool?((bool)value);
            }
            set { SetValue(IsCheckedProperty, value.HasValue ? value : null); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
                DependencyProperty.Register(
                        "IsChecked",
                        typeof(bool?),
                        typeof(ToggleButtonEx),
                        new FrameworkPropertyMetadata(
                                false,
                                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                                new PropertyChangedCallback(OnIsCheckedChanged)));

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToggleButtonEx button = (ToggleButtonEx)d;
            bool? oldValue = (bool?)e.OldValue;
            bool? newValue = (bool?)e.NewValue;

            if (newValue == true)
            {
                button.OnChecked(new RoutedEventArgs(CheckedEvent));
            }
            else if (newValue == false)
            {
                button.OnUnchecked(new RoutedEventArgs(UncheckedEvent));
            }
            else
            {
                button.OnIndeterminate(new RoutedEventArgs(IndeterminateEvent));
            }

            button.UpdateVisualState();
        }

        /// <summary>
        /// 设置按钮是否为三态
        /// </summary>
        public static readonly DependencyProperty IsThreeStateProperty =
                DependencyProperty.Register(
                        "IsThreeState",
                        typeof(bool),
                        typeof(ToggleButtonEx),
                        new FrameworkPropertyMetadata(false));
        [Bindable(true), Category("Behavior")]
        public bool IsThreeState
        {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetValue(IsThreeStateProperty, value); }
        }
        #endregion

        #region Events
        /// <summary>
        ///     Checked event
        /// </summary>
        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButtonEx));

        /// <summary>
        ///     Unchecked event
        /// </summary>
        public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButtonEx));

        /// <summary>
        ///     Indeterminate event
        /// </summary>
        public static readonly RoutedEvent IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButtonEx));


        /// <summary>
        ///     Add / Remove Checked handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Checked
        {
            add { AddHandler(CheckedEvent, value); }
            remove { RemoveHandler(CheckedEvent, value); }
        }

        /// <summary>
        ///     Add / Remove Unchecked handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Unchecked
        {
            add { AddHandler(UncheckedEvent, value); }
            remove { RemoveHandler(UncheckedEvent, value); }
        }

        /// <summary>
        ///     Add / Remove Indeterminate handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Indeterminate
        {
            add { AddHandler(IndeterminateEvent, value); }
            remove { RemoveHandler(IndeterminateEvent, value); }
        }
        #endregion

        #region Method
        /// <summary>
        /// 更新视觉效果
        /// </summary>
        private void UpdateVisualState()
        {
            var update = typeof(Control).GetMethod("UpdateVisualState", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
            update.Invoke(this, null);
        }

        /// <summary>
        /// 重写OnClick，定义新的表现形式
        /// </summary>
        protected override void OnClick()
        {
            OnToggle();
            base.OnClick();
        }

        /// <summary>
        /// 设置ToggleButton的状态
        /// </summary>
        protected virtual internal void OnToggle()
        {
            bool? isChecked;
            if (IsChecked == true)
                isChecked = IsThreeState ? (bool?)null : (bool?)false;
            else
                isChecked = IsChecked.HasValue;
            SetValue(IsCheckedProperty, isChecked);
        }

        /// <summary>
        /// OnChecked事件
        /// </summary>
        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// OnUnchecked事件
        /// </summary>
        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// OnIndeterminate 事件
        /// </summary>
        protected virtual void OnIndeterminate(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }


        #endregion

        #region Constructors
        static ToggleButtonEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleButtonEx), new FrameworkPropertyMetadata(typeof(ToggleButtonEx), FrameworkPropertyMetadataOptions.Inherits));
        }
        #endregion
    }
}
