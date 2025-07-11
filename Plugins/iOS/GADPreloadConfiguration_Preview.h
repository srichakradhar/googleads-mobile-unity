//
//  GADPreloadConfiguration_Preview.h
//  Google Mobile Ads SDK
//
//  Copyright © 2024 Google Inc. All rights reserved.
//

#import <GoogleMobileAds/GADAdFormat.h>
#import <GoogleMobileAds/GADRequest.h>

/// Configuration for preloading ads.
@interface GADPreloadConfiguration : NSObject

/// The ad unit ID.
@property(nonatomic, nonnull, readonly) NSString *adUnitID;

/// The GADRequest object.
@property(nonatomic, nonnull, readonly) GADRequest *request;

/// The format. Interstitial, rewarded, and app open ads are supported.
@property(nonatomic, readonly) GADAdFormat format;

/// The maximum amount of ads buffered for this configuration.
@property(nonatomic, readwrite) NSUInteger bufferSize;

/// Initializes a GADPreloadConfiguration with ad unit ID, request, and format.
- (nonnull instancetype)initWithAdUnitID:(nonnull NSString *)adUnitID
                                adFormat:(GADAdFormat)format
                                 request:(nonnull GADRequest *)request;

/// Initializes a GADPreloadConfiguration with ad unit ID, format, and default request object.
- (nonnull instancetype)initWithAdUnitID:(nonnull NSString *)adUnitID adFormat:(GADAdFormat)format;

@end
