﻿using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using AnorocMobileApp.Models;

namespace AnorocMobileApp.ViewModels.Navigation
{
    /// <summary>
    /// ViewModel for Me page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MeViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// To store the health profile data collection.
        /// </summary>
        private ObservableCollection<Me> cardItems;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MeViewModel" /> class.
        /// </summary>
        public MeViewModel()
        {
            cardItems = new ObservableCollection<Me>()
            {
                new Me()
                {
                    Category = "Calories Eaten",
                    CategoryValue = "13,100",
                    ImagePath = "CaloriesEaten.svg"
                },
                new Me()
                {
                    Category = "Heart Rate",
                    CategoryValue = "87 BPM",
                    ImagePath = "HeartRate.svg"
                },
                new Me()
                {
                    Category = "Water Consumed",
                    CategoryValue = "38.6 L",
                    ImagePath = "WaterConsumed.svg"
                },
                new Me()
                {
                    Category = "Sleep Duration",
                    CategoryValue = "7.3 H",
                    ImagePath = "SleepDuration.svg"
                }
            };

            this.ProfileImage = App.BaseImageUrl + "ProfileImage16.png";
            this.ProfileName = "Anoroc Van Looi";
            this.State = "Gauteng";
            this.Country = "South Africa";
            this.Age = "35";
            this.Weight = "159 Ibs";
            this.Height = "165 cm";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the health profile items collection.
        /// </summary>
        public ObservableCollection<Me> CardItems
        {
            get
            {
                return this.cardItems;
            }

            set
            {
                this.cardItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public string Height { get; set; }

        #endregion
    }
}