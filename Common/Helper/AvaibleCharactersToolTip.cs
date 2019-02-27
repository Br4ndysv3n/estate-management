// -----------------------------------------------------------------------
// <copyright file="AvailableCharactersToolTip.cs" company="PDV-Systeme GmbH">
// Copyright (C) 2012 PDV GmbH. Company Confidential.
// This work is fully protected as an unpublished work by copyright laws.
// Portions may be subject to pending patent applications. Its use
// requires a valid license from PDV GmbH.
// </copyright>
// -----------------------------------------------------------------------

namespace Common.Helper
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// The amount of avaible characters tooltip for textbox.
    /// </summary>
    public static class AvailableCharactersToolTip
    {
        public static ToolTip GetSpecialToolTip(DependencyObject obj)
        {

            var specialToolTip = obj.GetValue(SpecialToolTipProperty);
            if (specialToolTip != DependencyProperty.UnsetValue)
            {
                return specialToolTip as ToolTip;
            }

            return null;
        }

        public static void SetSpecialToolTip(DependencyObject obj, object value)
        {
            obj.SetValue(SpecialToolTipProperty, value);
        }

        public static readonly DependencyProperty SpecialToolTipProperty = DependencyProperty.RegisterAttached("SpecialToolTip", typeof(object), typeof(AvailableCharactersToolTip),
            new UIPropertyMetadata(null));


        public static bool GetIsAvaibleCharactersTooltip(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAvaibleCharactersTooltipProperty);
        }


        public static void SetIsAvaibleCharactersTooltip(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAvaibleCharactersTooltipProperty, value);
        }

        public static readonly DependencyProperty IsAvaibleCharactersTooltipProperty =
            DependencyProperty.RegisterAttached("IsAvaibleCharactersTooltip", typeof(bool), typeof(AvailableCharactersToolTip), new UIPropertyMetadata(false,
                (o, e) =>
                {
                    TextBox textBox = o as TextBox;
                    if (textBox != null)
                    {
                        if (e.NewValue != e.OldValue && (bool)e.NewValue)
                        {
                            textBox.TextChanged += TextBoxOnTextChanged;
                            textBox.SizeChanged += textBox_SizeChanged;
                            textBox.LostFocus += TextBoxOnLostFocus;
                            textBox.MouseLeave += TextBoxOnMouseLeave;
                           // textBox.MouseEnter += TextBoxMouseEnter;
                        }
                        else
                        {
                            textBox.TextChanged -= TextBoxOnTextChanged;
                            textBox.SizeChanged += textBox_SizeChanged;
                            textBox.LostFocus -= TextBoxOnLostFocus;
                            textBox.MouseLeave -= TextBoxOnMouseLeave;
                            //textBox.MouseEnter -= TextBoxMouseEnter;
                        }
                    }
                }));

        private static void textBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ToolTip tooltip = GetSpecialToolTip(sender as TextBox);
            if (tooltip != null && tooltip.IsOpen == true)
            {
                HideTooltip(sender as TextBox);
                ShowTooltip(sender as TextBox);
            }
        }



        private static void TextBoxMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            ShowTooltip(sender as TextBox);
        }

        private static void TextBoxOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            HideTooltip(sender as TextBox);
        }

        private static void TextBoxOnLostFocus(object sender, RoutedEventArgs e)
        {
            HideTooltip(sender as TextBox);
        }

        private static void TextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {

            TextBox textBox = sender as TextBox;
            if (textBox != null && ((textBox.IsEnabled && !textBox.IsReadOnly) && textBox.MaxLength > 0) && textBox.IsFocused)
            {
                ShowTooltip(textBox);
            }
        }

        private static void HideTooltip(TextBox sender)
        {
            ToolTip tooltip = GetSpecialToolTip(sender);
            if (tooltip != null)
            {
                tooltip.IsOpen = false;
                //tooltip.Visibility = Visibility.Collapsed;
            }
        }

        private static void ShowTooltip(TextBox sender)
        {
            ToolTip tooltip = GetSpecialToolTip(sender);
            if (tooltip != null)
            {
                tooltip.PlacementTarget = sender;
                int length = PropertyHelper.ConvertFromString(tooltip.Content.ToString(), 0, null);
                int percentage = 0;
                if (length > 0)
                {
                    percentage = 100 / (sender.MaxLength / length);
                }
                if (percentage >= 25) tooltip.Tag = "#5e7531";
                else if (percentage >= 10) tooltip.Tag = "#965f00";
                else tooltip.Tag = "#c70b0b";
                tooltip.IsOpen = true;
                //tooltip.Visibility = Visibility.Visible;
            }
        }
    }
}
