//
//  GADRewardedAd_Preview.h
//  Google Mobile Ads SDK
//
//  Copyright © 2024 Google Inc. All rights reserved.
//

#import <GoogleMobileAds/GADMobileAds.h>
#import <GoogleMobileAds/GADRewardedAd.h>

@interface GADRewardedAd ()

/// Returns if a rewarded ad is preloaded for the given ad unit ID.
+ (BOOL)isPreloadedAdAvailable:(nonnull NSString *)adUnitID;

/// Returns a rewarded ad associated with the ad unit ID. Returns nil if
/// an ad is not available.
+ (nullable GADRewardedAd *)preloadedAdWithAdUnitID:(nonnull NSString *)adUnitID;

@end
