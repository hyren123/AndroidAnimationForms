using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AndroidAnimationForms
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            Label Title = new Label() { Text = "Animation Tester" };

            Switch switchAnimate = new Switch();

            ViewCell aniamteSwitch = new ViewCell()
            {
                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Label{ Text = "Animate"},
                        switchAnimate
                    }
                }
            };

            ViewCell AnimateCell = new ViewCell()
            {
                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Label{ Text = "I'm to be Animated!"},
                        new Switch()
                    }
                }
            };

            AnimateCell.View.HeightRequest = 0;
            AnimateCell.View.IsVisible = false;


            TableRoot root = new TableRoot()
            {
                new TableSection()
                {
                    aniamteSwitch,
                    AnimateCell
                }
            };

            TableView table = new TableView()
            {
                Root = root
            };

            ScrollView scroll = new ScrollView() { Content = table};

            StackLayout stack = new StackLayout()
            {
                Children =
                {
                    Title,
                    scroll
                }
            };

            switchAnimate.Toggled += (sender, e) => 
            {
                AnimationChooser(switchAnimate.IsToggled, AnimateCell, root);
            };

            Content = stack;
		}

        private void AnimationChooser(bool enable, ViewCell animateTarget, TableRoot tableRoot)
        {
            VisualElement visual = animateTarget.View;
            if (enable)
            {
                AnimateLinearAppearing(visual, tableRoot);
            }
            else AnimateLinearDisappearing(visual, tableRoot);
        }

        private void AnimateLinearAppearing(VisualElement visual, TableRoot tableRoot)
        {
            visual.IsVisible = true;

            Animation animation = new Animation(d => 
            {
                visual.HeightRequest = d;
                TableRootRefresher(tableRoot);
            }, 0, Cell.DefaultCellHeight);

            animation.Commit(this, "AnimateAppearing", 16, 250, Easing.Linear, (d, b) => { animation = null; });
        }

        private void AnimateLinearDisappearing(VisualElement visual, TableRoot tableRoot)
        {
            Animation animation = new Animation(d => 
            {
                visual.HeightRequest = d;
                TableRootRefresher(tableRoot);
            }, visual.Height, 0);

            animation.Commit(this, "AnimateAppearing", 16, 250, Easing.Linear, (d, b) => 
            {
                visual.IsVisible = false;
                TableRootRefresher(tableRoot);
                animation = null;
            });
        }

        private void TableRootRefresher(TableRoot tableRoot)
        {
            var temp = tableRoot;

            tableRoot = null;

            tableRoot = temp;
        }
	}
}
