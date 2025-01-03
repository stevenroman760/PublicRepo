using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PublicRepo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicRepo.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        public ObservableCollection<TaskModel> TaskList { get; set; } = new();
        public List<TaskModel> _allTasksList = new();

        private TaskModel _draggedItem;

        [ObservableProperty]
        private int _newTaskCount;

        [ObservableProperty]
        private int _inProgressCount;

        [ObservableProperty]
        private int _reviewCount;

        [ObservableProperty]
        private int _doneCount;

        [ObservableProperty]
        private int _selectedOption;

        [ObservableProperty]
        private bool _isBusy;

        public DashboardViewModel() 
        {
            _allTasksList.Add(new TaskModel() { TaskName = "Task 1", TaskStatus = (int)TaskStatusOption.NewTask, TaskId = 1 });
            _allTasksList.Add(new TaskModel() { TaskName = "Task 2", TaskStatus = (int)TaskStatusOption.NewTask, TaskId = 2 });
            _allTasksList.Add(new TaskModel() { TaskName = "Task 3", TaskStatus = (int)TaskStatusOption.InProgress, TaskId = 3 });
            _allTasksList.Add(new TaskModel() { TaskName = "Task 4", TaskStatus = (int)TaskStatusOption.InProgress, TaskId = 4 });
            _allTasksList.Add(new TaskModel() { TaskName = "Task 5", TaskStatus = (int)TaskStatusOption.InReview, TaskId = 5 });
            _allTasksList.Add(new TaskModel() { TaskName = "Task 6", TaskStatus = (int)TaskStatusOption.InReview, TaskId = 6 });
            _allTasksList.Add(new TaskModel() { TaskName = "Task 7", TaskStatus = (int)TaskStatusOption.Done, TaskId = 7 });

            AddTaskList();
        }

        public void AddTaskList()
        { 
            var filterTaskList = _allTasksList.Where(f => f.TaskStatus == SelectedOption).ToList();

            TaskList.Clear();
            foreach(var task in filterTaskList)
            {
                TaskList.Add(task);
            }

            SetCount();
        }

        private void SetCount()
        {
            NewTaskCount = _allTasksList.Count(f => f.TaskStatus == (int)TaskStatusOption.NewTask);
            InProgressCount = _allTasksList.Count(f => f.TaskStatus == (int)TaskStatusOption.InProgress);
            ReviewCount = _allTasksList.Count(f => f.TaskStatus == (int)TaskStatusOption.InReview);
            DoneCount = _allTasksList.Count(f => f.TaskStatus == (int)TaskStatusOption.Done);
        }

        [RelayCommand]
        public void FilterTaskList(string optionStr)
        { 
            int option = Convert.ToInt32(optionStr);
            SelectedOption = -1;
            SelectedOption = option;

            AddTaskList();
        }

        [RelayCommand]
        public void DragStarted(TaskModel task)
        {
            _draggedItem = task;
        }

        [RelayCommand]
        public async Task ItemDropped(string key)
        {
            int option = Convert.ToInt32(key);

            if (SelectedOption == option) return;
            
            IsBusy = true;
            await Task.Delay(500);


            if (_draggedItem != null)
            {
                var currentItem = _allTasksList.Where(f => f.TaskId == _draggedItem.TaskId).FirstOrDefault();
                currentItem.TaskStatus = option;

                AddTaskList();
            }
            IsBusy = false;
        }
    }
}
