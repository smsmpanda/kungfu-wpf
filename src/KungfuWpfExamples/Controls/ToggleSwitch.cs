using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KungfuWpfExamples
{
    public class ToggleSwitch : CheckBox
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch()
        {
            Checked += ToggleSwitch_Checked;
            Unchecked += ToggleSwitch_Unchecked;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            AnimateButton(false);
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            AnimateButton(true);
        }

        private void AnimateButton(bool isChecked)
        {
            if (GetTemplateChild("button") is Border button)
            {
                Storyboard storyboard = new Storyboard();
                ThicknessAnimation animation = new ThicknessAnimation();
                animation.Duration = new Duration(TimeSpan.FromSeconds(.4));
                animation.EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut };

                if (isChecked)
                {
                    animation.From = new Thickness(4, 0, 0, 0);
                    animation.To = new Thickness(ActualWidth - (ActualHeight - 8) - 4, 0, 0, 0);
                }
                else
                {
                    animation.To = new Thickness(4, 0, 0, 0);
                    animation.From = new Thickness(ActualWidth - (ActualHeight - 8) - 4, 0, 0, 0);
                }
                Storyboard.SetTarget(animation, button);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                storyboard.Children.Add(animation);
                //if (isChecked)
                //{
                //    Resources.Remove("toLeft");
                //    Resources["toRight"] = storyboard;
                //}
                //else
                //{
                //    Resources.Remove("toRight");
                //    Resources["toLeft"] = storyboard;
                //}
                storyboard.Begin(this);
            }
        }
    }
}
