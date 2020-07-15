# TrainingFinder
ASP.NET Core with NativeScript(using Angular) app to find new people to train with on gym

[![Build Status](https://dev.azure.com/szymondomalik/Training%20Finder/_apis/build/status/dburnat.TrainingFinder?branchName=master)](https://dev.azure.com/szymondomalik/Training%20Finder/_build/latest?definitionId=3&branchName=master)
=======
### Screenshots
Home            |  Gym
:-------------------------:|:-------------------------:
![](https://github.com/dburnat/TrainingFinder/blob/master/Screenshots/home.jpg)  |  ![](https://github.com/dburnat/TrainingFinder/blob/master/Screenshots/gyms.jpg)

### Configure Google API Key
1. Go to the [Google Developers Console](https://console.developers.google.com/), create a project, and enable the **Google Maps SDK for Android**, **Google Maps SDK for iOS** and **Geocoding API**. Then under credentials, create an API key.
2. (Server)Edit appsettings.json which is located in `TrainingFinder\TrainingFinder`
```json
{
  "AppSettings": {
    "Secret": "USER_HASHING_KEY",
    "GoogleApiKey": "PUT_YOUR_API_KEY_HERE"
  },
}
```
3. (iOS)Edit environment.ts file which is located in `TrainingFinder\TrainingFinderMobile\src\environments`
```javascript
export const environment = {
    production: false,
    apiUrl: 'YOUR_API_URL_HERE',
    googleApiKey: "YOUR_GOOGLE_API_KEY_HERE"
};

```
4. (Android)Edit **nativescript_google_maps_api.xml** which is located in `TrainingFinder\TrainingFinderMobile\App_Resources\Android\src\main\res\values`
```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <string name="nativescript_google_maps_api_key">YOUR_GOOGLE_API_KEY_HERE</string>
</resources>
```
