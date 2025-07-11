//
//  GADAppOpenAd_Preview.h
//  Google Mobile Ads SDK
//
//  Copyright © 2024 Google Inc. All rights reserved.
//

#import <GoogleMobileAds/GADAppOpenAd.h>
#import <GoogleMobileAds/GADMobileAds.h>

@interface GADAppOpenAd ()

/// Returns if an app open ad is preloaded for the given ad unit ID.
+ (BOOL)isPreloadedAdAvailable:(nonnull NSString *)adUnitID;

/// Returns the preloaded app open ad corresponding to the given ad unit ID. Returns nil if
/// an ad is not available.
+ (nullable GADAppOpenAd *)preloadedAdWithAdUnitID:(nonnull NSString *)adUnitID;

@end
