// Copyright (C) 2021 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;

using UnityEngine;

using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
    /// <summary>
    /// App open ads are used to display ads when users launch your app.
    /// Unlike interstitial ads, app open ads make it easy to provide app branding so that users
    /// understand the context in which they see the ad.
    /// </summary>
    public class AppOpenAd
    {
        /// <summary>
        /// Raised when the ad is estimated to have earned money.
        /// </summary>
        public event Action<AdValue> OnAdPaid;

        /// <summary>
        /// Raised when a click is recorded for an ad.
        /// </summary>
        public event Action OnAdClicked;

        /// <summary>
        /// Raised when an impression is recorded for an ad.
        /// </summary>
        public event Action OnAdImpressionRecorded;

        /// <summary>
        /// Raised when an ad opened full-screen content.
        /// </summary>
        public event Action OnAdFullScreenContentOpened;

        /// <summary>
        /// Raised when the ad closed full-screen content.
        /// On iOS, this event is only raised when an ad opens an overlay, not when opening a new
        /// application such as Safari or the App Store.
        /// </summary>
        public event Action OnAdFullScreenContentClosed;

        /// <summary>
        /// Raised when the ad failed to open full-screen content.
        /// </summary>
        public event Action<AdError> OnAdFullScreenContentFailed;

        private static IAppOpenAdClient _preloadAppOpenAdClient;
        private IAppOpenAdClient _client;
        private bool _canShowAd;

        private AppOpenAd(IAppOpenAdClient client)
        {
            _canShowAd = true;
            _client = client;

            RegisterAdEvents();
        }

        /// <summary>
        /// Loads an app open ad.
        /// </summary>
        public static void Load(string adUnitId,
                                AdRequest request,
                                Action<AppOpenAd, LoadAdError> adLoadCallback)
        {
            if (adLoadCallback == null)
            {
                UnityEngine.Debug.LogError("adLoadCallback is null. No ad was loaded.");
                return;
            }

            if (_preloadAppOpenAdClient == null)
            {
                _preloadAppOpenAdClient = MobileAds.GetClientFactory().BuildAppOpenAdClient();
                _preloadAppOpenAdClient.CreateAppOpenAd();
            }
            _preloadAppOpenAdClient.OnAdLoaded += (sender, args) =>
            {
                MobileAds.RaiseAction(() =>
                {
                    adLoadCallback(new AppOpenAd(_preloadAppOpenAdClient), null);
                });
            };
            _preloadAppOpenAdClient.OnAdFailedToLoad += (sender, args) =>
            {
                LoadAdError loadAdError = new LoadAdError(args.LoadAdErrorClient);
                MobileAds.RaiseAction(() =>
                {
                    adLoadCallback(null, loadAdError);
                });
            };
            _preloadAppOpenAdClient.LoadAd(adUnitId, request);
        }

        /// <summary>
        /// Verify if an ad is preloaded and available to show.
        /// </summary>
        /// <param name="adUnitId">The ad Unit Id of the ad to verify. </param>
        public static bool IsAdAvailable(string adUnitId)
        {
            if (string.IsNullOrEmpty(adUnitId))
            {
                Debug.LogError("adUnitId cannot be null or empty.");
                return false;
            }
            if (_preloadAppOpenAdClient == null)
            {
                _preloadAppOpenAdClient = MobileAds.GetClientFactory().BuildAppOpenAdClient();
            }
            return _preloadAppOpenAdClient == null ? false :
                    _preloadAppOpenAdClient.IsAdAvailable(adUnitId);
        }

        /// <summary>
        /// Returns the next pre-loaded app open ad and null if no ad is available.
        /// </summary>
        /// <param name="adUnitId">The ad Unit ID of the ad to poll.</param>
        public static AppOpenAd PollAd(string adUnitId)
        {
            if (string.IsNullOrEmpty(adUnitId))
            {
                Debug.LogError("adUnitId cannot be null or empty.");
                return null;
            }
            if (_preloadAppOpenAdClient == null)
            {
                _preloadAppOpenAdClient = MobileAds.GetClientFactory().BuildAppOpenAdClient();
            }
            _preloadAppOpenAdClient.CreateAppOpenAd();
            return _preloadAppOpenAdClient == null ? null :
                    new AppOpenAd(_preloadAppOpenAdClient.PollAd(adUnitId));
        }

        /// <summary>
        /// Returns true if the ad is loaded and not shown.
        /// </summary>
        public bool CanShowAd()
        {
            return _client != null && _canShowAd;
        }

        /// <summary>
        /// Shows an app open ad.
        /// </summary>
        public void Show()
        {
            if (CanShowAd())
            {
                _canShowAd = false;
                _client.Show();
            }
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void Destroy()
        {
            _canShowAd = false;
            // This is a safeguard to prevent errors caused by incorrect
            // publisher use of Destroy() API.
            if (_client != null)
            {
                _client.DestroyAppOpenAd();
            }
        }

        /// <summary>
        /// Returns the ad unit ID.
        /// </summary>
        public string GetAdUnitID()
        {
            return _client != null ? _client.GetAdUnitID() : null;
        }

        /// <summary>
        /// Returns the ad request response info.
        /// </summary>
        public ResponseInfo GetResponseInfo()
        {
            return _client == null ? null : new ResponseInfo(_client.GetResponseInfoClient());
        }

        private void RegisterAdEvents()
        {
            _client.OnAdClicked += () =>
            {
                MobileAds.RaiseAction(() =>
                {
                    if (OnAdClicked != null)
                    {
                        OnAdClicked();
                    }
                });
            };

            _client.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                MobileAds.RaiseAction(() =>
                {
                    if (OnAdFullScreenContentClosed != null)
                    {
                        OnAdFullScreenContentClosed();
                    }
                });
            };

            _client.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                MobileAds.RaiseAction(() =>
                {
                    if (OnAdFullScreenContentOpened != null)
                    {
                        OnAdFullScreenContentOpened();
                    }
                });
            };

            _client.OnAdDidRecordImpression += (sender, args) =>
            {
                MobileAds.RaiseAction(() =>
                {
                    if (OnAdImpressionRecorded != null)
                    {
                        OnAdImpressionRecorded();
                    }
                });
            };

            _client.OnAdFailedToPresentFullScreenContent += (sender, error) =>
            {
                AdError adError = new AdError(error.AdErrorClient);
                MobileAds.RaiseAction(() =>
                {
                    if (OnAdFullScreenContentFailed != null)
                    {
                        OnAdFullScreenContentFailed(adError);
                    }
                });
            };

            _client.OnPaidEvent += (adValue) =>
            {
                MobileAds.RaiseAction(() =>
                {
                    if (OnAdPaid != null)
                    {
                        OnAdPaid(adValue);
                    }
                });
            };
        }
    }
}
