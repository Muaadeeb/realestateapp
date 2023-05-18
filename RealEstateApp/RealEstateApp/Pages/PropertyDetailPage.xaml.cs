using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private bool IsBookmarkEnabled;
    private int propertyId;
    private int bookmarkId;

	public PropertyDetailPage(int propertyId)
	{
		InitializeComponent();
		GetPropertyDetail(propertyId);
        this.propertyId = propertyId;

	}

    private async void GetPropertyDetail(int propertyId)
    {
        var propertyDetail = await ApiService.GetPropertyDetail(propertyId);
        LblPrice.Text = $"$ {propertyDetail.Price}";
        LblDescription.Text = propertyDetail.Detail;
        LblAddress.Text = propertyDetail.Address;
        ImgProperty.Source = propertyDetail.FullImageUrl;
        phoneNumber = propertyDetail.Phone;

        if (propertyDetail.Bookmark is null)
        {
            ImgbookmarkBtn.Source = "bookmark_empty_icon";
            IsBookmarkEnabled = false;
        }
        else
        {
            ImgbookmarkBtn.Source= propertyDetail.Bookmark.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
            bookmarkId = propertyDetail.Bookmark.Id;
            IsBookmarkEnabled = true;
        }
    }

    private void TapMessage_Tapped(Object sender, TappedEventArgs e)
    {
        var message = new SmsMessage("Hi! I am interested in your property", phoneNumber);
        Sms.ComposeAsync(message);
    }

    private void TapCall_Tapped(Object sender, TappedEventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }
    
    private void ImgbackBtn_Clicked(Object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    async void ImgbookmarkBtn_Clicked(Object sender, EventArgs e)
    {
        if (IsBookmarkEnabled is false)
        {
            // add bookmark
            var addBookMark = new AddBookmark()
            {
                User_Id = Preferences.Get(AppSettings.UserId, 0),
                PropertyId = propertyId
            };

            var response = await ApiService.AddBookmark(addBookMark);
            if (response)
            {
                ImgbookmarkBtn.Source = "bookmark_fill_icon";
                IsBookmarkEnabled = true;
            }
        }
        else
        {
            // delete a bookmark
            var response = await ApiService.DeleteBookmark(bookmarkId);
            if (response)
            {
                ImgbookmarkBtn.Source = "bookmark_empty_icon";
                IsBookmarkEnabled = false;
            }
        }
    }
}