using Microsoft.Win32;
using SwimLane.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Swimlane.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        #region Fields

        private ICommand _AddCommand;
        private ICommand _RemoveCommand;
        #endregion
        public DiagramVM()
        {
            SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.None,
            };

            SelectedItems = new SelectorViewModel();

            HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };
            VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            InitializeDiagram();
            AddCommand = new Command(OnAdd);
            RemoveCommand = new Command(OnRemove);

        }

        private void InitializeDiagram()
        {
            //Initialize SwimlaneCollection to SfDiagram
            this.Swimlanes = new SwimlaneCollection();

            //Creating the SwimlaneViewModel
            SwimlaneViewModel swimlane = new SwimlaneViewModel()
            {
                UnitWidth = 450,
                UnitHeight = 120,
                OffsetX = 300,
                OffsetY = 150,
                Orientation = Orientation.Horizontal,
            };
            //Creating Header for SwimlaneViewModel
            swimlane.Header = new SwimlaneHeader()
            {
                UnitHeight = 32,
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = "SALES PROCESS FLOW CHART"
                },
            };

            swimlane.Lanes = new LaneCollection()
            {
                new LaneViewModel()
                {
                    UnitHeight=100,
                    Header=new SwimlaneHeader()
                    {
                        UnitHeight=30,
                        Annotation=new AnnotationEditorViewModel(){Content="Consumer"}                        
                    }
                }
            };

            //Add Swimlane to Swimlanes property of the Diagram
            (this.Swimlanes as SwimlaneCollection).Add(swimlane);
        }

        #region Commands

        public ICommand AddCommand
        {
            get { return _AddCommand; }
            set { _AddCommand = value; }
        }

        public ICommand RemoveCommand
        {
            get { return _RemoveCommand; }
            set { _RemoveCommand = value; }
        }


        #endregion
        #region Helper Methods

        private void OnAdd(object obj)
        {
            var swimlane = (this.Swimlanes as SwimlaneCollection).FirstOrDefault() as SwimlaneViewModel;
            if (swimlane != null)
            {
                (swimlane.Lanes as LaneCollection).Add(new LaneViewModel() { UnitHeight = 100 });
            }
        }
        private void OnRemove(object obj)
        {
            var swimlane = (this.Swimlanes as SwimlaneCollection).FirstOrDefault() as SwimlaneViewModel;
            if (swimlane != null && (swimlane.Lanes as LaneCollection).Count>1)
            {
                (swimlane.Lanes as LaneCollection).Remove((swimlane.Lanes as LaneCollection).LastOrDefault());
            }
        }



        #endregion

    }
}
