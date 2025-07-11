//
//  GADInterstitialAd_Preview.h
//  Google Mobile Ads SDK
//
//  Copyright © 2024 Google Inc. All rights reserved.
//

#import <GoogleMobileAds/GADInterstitialAd.h>
#import <GoogleMobileAds/GADMobileAds.h>

@interface GADInterstitialAd ()

/// Returns whether an interstitial ad is preloaded for the given ad unit ID.
+ (BOOL)isPreloadedAdAvailable:(nonnull NSString *)adUnitID;

/// Returns a preloaded interstitial ad corresponding to the given ad unit ID. Returns nil if
/// an ad is not available.
+ (nullable GADInterstitialAd *)preloadedAdWithAdUnitID:(nonnull NSString *)adUnitID;

@end
