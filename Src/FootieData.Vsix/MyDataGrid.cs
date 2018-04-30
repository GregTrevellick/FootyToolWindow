using FootieData.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FootieData.Vsix
{
    public class MyDataGrid : DataGrid
    {
        public MyDataGrid()
        {
            var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8F8F8"));//("#F2F2F2"));//("#FFFFF0"));
            AlternatingRowBackground = color;
            CanUserAddRows = false;
            ColumnHeaderHeight = 24;
            GridLinesVisibility = DataGridGridLinesVisibility.None;
            RowHeaderWidth = 0;
        }

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                base.OnAutoGeneratingColumn(e);

                var propDescr = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
                e.Column.Header = propDescr?.Description;

                var style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));
                style.Setters.Add(new Setter(ToolTipService.ToolTipProperty, "Click to sort"));

                e.Column.HeaderStyle = style;
            }
            catch (Exception)
            {
                Logger.Log("No data grid column heading found");
            }
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);

            var entityBase = (EntityBase)e.Row.Item;
            var hidePoliteError = string.IsNullOrEmpty(entityBase.PoliteError);

            foreach (var item in this.Columns)
            {
                SetPoliteErrorMessageVisibility(entityBase, hidePoliteError, item);

                var _style = new Style();
               
                if (IsNumericalColumn(item))
                {
                    _style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
                }

                if (IsHomeColumn(item))
                {
                    var homeAwayFontColour = Brushes.SlateGray;              
                    _style.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));
                }

                if (IsAwayColumn(item))
                {
                    var homeAwayFontColour = Brushes.SlateGray;
                    _style.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));
                }

                item.CellStyle = _style;
                item.HeaderStyle = _style;
            }
        }

        private static bool IsHomeColumn(DataGridColumn item)
        {
            var isHomeColumn =
                item.SortMemberPath == nameof(Standing.HomeDraws) ||
                item.SortMemberPath == nameof(Standing.HomeGoalsAgainst) ||
                item.SortMemberPath == nameof(Standing.HomeGoalsFor) ||
                item.SortMemberPath == nameof(Standing.HomeLosses) ||
                item.SortMemberPath == nameof(Standing.HomePlayed) ||
                item.SortMemberPath == nameof(Standing.HomePoints) ||
                item.SortMemberPath == nameof(Standing.HomeGoalDiff) ||
                item.SortMemberPath == nameof(Standing.HomeWins);
            return isHomeColumn;
        }

        private static bool IsAwayColumn(DataGridColumn item)
        { 
            var isAwayColumn =
                item.SortMemberPath == nameof(Standing.AwayDraws) ||
                item.SortMemberPath == nameof(Standing.AwayGoalsAgainst) ||
                item.SortMemberPath == nameof(Standing.AwayGoalsFor) ||
                item.SortMemberPath == nameof(Standing.AwayLosses) ||
                item.SortMemberPath == nameof(Standing.AwayPlayed) ||
                item.SortMemberPath == nameof(Standing.AwayPoints) ||
                item.SortMemberPath == nameof(Standing.AwayGoalDiff) ||
                item.SortMemberPath == nameof(Standing.AwayWins);
            return isAwayColumn;
        }

        private static bool IsNumericalColumn(DataGridColumn item)
        {
            var isNumericalColumn =
                item.SortMemberPath == nameof(FixturePast.Date) ||
                item.SortMemberPath == nameof(FixturePast.GoalsHome) ||
                item.SortMemberPath == nameof(FixtureFuture.Date) ||
                item.SortMemberPath == nameof(Standing.Against) ||
                item.SortMemberPath == nameof(Standing.AwayDraws) ||
                item.SortMemberPath == nameof(Standing.AwayGoalsAgainst) ||
                item.SortMemberPath == nameof(Standing.AwayGoalsFor) ||
                item.SortMemberPath == nameof(Standing.AwayLosses) ||
                item.SortMemberPath == nameof(Standing.AwayPlayed) ||
                item.SortMemberPath == nameof(Standing.AwayPoints) ||
                item.SortMemberPath == nameof(Standing.AwayWins) ||
                item.SortMemberPath == nameof(Standing.AwayGoalDiff) ||
                item.SortMemberPath == nameof(Standing.HomeGoalDiff) ||
                item.SortMemberPath == nameof(Standing.Diff) ||
                item.SortMemberPath == nameof(Standing.Draws) ||
                item.SortMemberPath == nameof(Standing.For) ||
                item.SortMemberPath == nameof(Standing.HomeDraws) ||
                item.SortMemberPath == nameof(Standing.HomeGoalsAgainst) ||
                item.SortMemberPath == nameof(Standing.HomeGoalsFor) ||
                item.SortMemberPath == nameof(Standing.HomeLosses) ||
                item.SortMemberPath == nameof(Standing.HomePlayed) ||
                item.SortMemberPath == nameof(Standing.HomePoints) ||
                item.SortMemberPath == nameof(Standing.HomeWins) ||
                item.SortMemberPath == nameof(Standing.Losses) ||
                item.SortMemberPath == nameof(Standing.Played) ||
                item.SortMemberPath == nameof(Standing.Points) ||
                item.SortMemberPath == nameof(Standing.Rank) ||
                item.SortMemberPath == nameof(Standing.Wins);
            return isNumericalColumn;
        }

        private static void SetPoliteErrorMessageVisibility(EntityBase entityBase, bool hidePoliteError, DataGridColumn item)
        {
            var isPoliteErrorColumn = item.SortMemberPath == nameof(entityBase.PoliteError);
            var visibility = Visibility.Collapsed;

            if (isPoliteErrorColumn)
            {
                if (!hidePoliteError)
                {
                    visibility = Visibility.Visible;
                }
            }
            else
            {
                if (hidePoliteError)
                {
                    visibility = Visibility.Visible;
                }
            }

            item.Visibility = visibility;
        }
    }
}