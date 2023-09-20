## To fix the issue i have edited the two files which the first ir direectly related to the TextFlightView.xaml.cs & LanguageText.cs

![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/6d005e62-6dc3-44c0-8525-175e3617aa6e)

### First inside the TextFlightView.xaml.cs
- i have added the one line code which opens up the new RadWindow when the help button is clicked.
  
![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/5d5ad339-8120-4431-9e62-5d83b992d50c)

    RadWindow.Alert(new DialogParameters { Owner = this, Header = "Help", Content = LanguageText.CurrentLanguage[LanguageText.TextType.UI_TestFlight_HELPAlert] });
- RadWindow.Alert: Display dialog **method**.
DialogParameters: Dialog settings **object**.
Owner = this: Dialog context where the code is executed.
Header = "Help": Dialog title.
Content: Dialog message.
LanguageText: Text localization.
UI_TestFlight_HELPAlert: Message key.

        private void TestFlightHelpButton(object sender, NavigationButtonsEventArgs e)
        {
        if (this.spHelp.Visibility == Visibility.Visible)
        {
            Hide_HelpModelView();
        }
        else
        {
            Show_HelpModelView();
        }
        RadWindow.Alert(new DialogParameters { Owner = this, Header = "Help", Content = LanguageText.CurrentLanguage[LanguageText.TextType.UI_TestFlight_HELPAlert] });
        }



### Second, inside the LanguageText.cs file
- i have added below image like 5 lines to add the text inside window
  
![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/dcf6c663-30d3-4a28-b635-41ccff425dd1)

    UI_TestFlight_HELPAlert,

    enText.Add(TextType.UI_TestFlight_HELPAlert, "The basic flight test is a procedure to determine the presence or absence of equipment\n" +
    " malfunctions by conducting basic flight tests before operating the drone.\n");

- UI_TestFlight_HELPAlert is a unique identifier or key. enText.Add(...) associates the key with an English text description.

![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/2dedbd9a-8d9f-4241-9437-72054da78769)

![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/115e159e-04c1-4f66-9bae-241b1879d6a2)



### Solution finding with similar case code
![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/f13a1ff4-d702-441e-8acb-5061ecfb2801)

- ctrl + shift + f, to find the Menu_HelpItem and f12 to enter where it is located , and shift to there the location of that from below system search
![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/f759cc65-5a53-49de-a0fb-e8974cba1e21)

![image](https://github.com/UbaydullohML/Bug_FIX_Ales/assets/75980506/b48122f4-554e-42b5-af7e-3860160c4bd1)
- by searching the HelpMenu_EventHandler

