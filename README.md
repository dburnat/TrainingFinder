# TrainingFinder
ASP.NET Core with Angular app to find new people to train with on gym

[![Build Status](https://dev.azure.com/szymondomalik/Training%20Finder/_apis/build/status/dburnat.TrainingFinder?branchName=master)](https://dev.azure.com/szymondomalik/Training%20Finder/_build/latest?definitionId=3&branchName=master)
=======

### Configure Google Maps API Key
1. Go to the [Google Developers Console](https://console.developers.google.com/), create a project, and enable the **Google Maps SDK for Android** and **Google Maps SDK for iOS**. Then under credentials, create an API key.
2. (iOS)Edit environment.ts file which is located in `TrainingFinder\TrainingFinderMobile\src\environments`
```javascript
export const environment = {
    production: false,
    apiUrl: 'YOUR_API_URL_HERE',
    googleApiKey: "YOUR_GOOGLE_API_KEY_HERE"
};

```
3. (Android)Edit **nativescript_google_maps_api.xml** which is located in `TrainingFinder\TrainingFinderMobile\App_Resources\Android\src\main\res\values`
```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <string name="nativescript_google_maps_api_key">YOUR_GOOGLE_API_KEY_HERE</string>
</resources>
```
