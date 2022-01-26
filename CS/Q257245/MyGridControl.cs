using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DXSample {
    [ToolboxItem(true)]
    public class MyGridControl : GridControl {
        protected override BaseView CreateDefaultView() {
            return CreateView("MyGridView");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection) {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new MyGridViewInfoRegistrator());
        }
    }

    public class MyGridViewInfoRegistrator : GridInfoRegistrator {
        public override string ViewName => "MyGridView";

        public override BaseView CreateView(GridControl grid) {
            return new MyGridView(grid);
        }
    }

    public class MyGridView : DevExpress.XtraGrid.Views.Grid.GridView {
        public MyGridView() {
        }

        public MyGridView(GridControl grid) : base(grid) {
        }

        protected override string ViewName => "MyGridView";

        [DefaultValue(-1)]
        public int MaxSelectedRowsCount { get; set; } = -1;

        public override void SelectRow(int rowHandle) {
            if (SelectedRowsCount == MaxSelectedRowsCount)
                return;
            base.SelectRow(rowHandle);
        }

        public override void SelectRange(int startRowHandle, int endRowHandle) {
            if(startRowHandle == SelectionAnchorRowHandle && MaxSelectedRowsCount > -1) {
                if(endRowHandle >= startRowHandle) {
                    endRowHandle = Math.Min(endRowHandle, startRowHandle + MaxSelectedRowsCount - 1);
                } else {
                    endRowHandle = Math.Max(endRowHandle, startRowHandle - MaxSelectedRowsCount + 1);
                }
            }
            base.SelectRange(startRowHandle, endRowHandle);
        }

        public override void SelectAll() {
            if (MaxSelectedRowsCount == -1)
                base.SelectAll();
        }
    }
}
