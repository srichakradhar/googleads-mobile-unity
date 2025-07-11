// Copyright 2014 Google Inc. All Rights Reserved.

#import <Foundation/Foundation.h>

#import <GoogleMobileAds/GoogleMobileAds.h>
#import "GADMobileAds_Preview.h"
#import "GADUPreloadConfiguration.h"
#import "GADUObjectCache.h"
#import "GADUTypes.h"

@interface GADUMobileAds : NSObject <GADPreloadEventDelegate>

- (nonnull id)initWithMobileAdsClientReference:(_Nonnull GADUTypeMobileAdsClientRef *_Nonnull)mobileAdsClient;

@property(nonatomic, assign) GADUTypeMobileAdsClientRef _Nonnull * _Nonnull mobileAdsClient;

/// Called when an ad becomes available for the configuration.
@property(nonatomic, assign, nullable) GADUAdAvailableForPreloadConfigurationCallback
    adAvailableForPreloadConfigurationCallback;

/// Called when the last available ad is exhausted for the configuration.
@property(nonatomic, assign, nullable) GADUAdsExhaustedForPreloadConfigurationCallback
    adsExhaustedForPreloadConfigurationCallback;

@end
