// Copyright (C) 2024 Google LLC
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

using System.Collections.Generic;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
    internal class AdRequestClient : IAdRequestClient
    {
        private AndroidJavaObject _adRequest;

        public AdRequestClient(AndroidJavaObject adRequest)
        {
            this._adRequest = adRequest;
        }

        public HashSet<string> GetKeywords()
        {
            AndroidJavaObject keywords = _adRequest.Call<AndroidJavaObject>("getKeywords");
            keywords = keywords.Call<AndroidJavaObject>("iterator");
            HashSet<string> keywordsSet = new HashSet<string>();
            while (keywords.Call<bool>("hasNext"))
            {
                keywordsSet.Add(keywords.Call<string>("next"));
            }
            return keywordsSet;
        }

        public override string ToString()
        {
            return _adRequest.Call<string>("getAdString");
        }

    }
}
