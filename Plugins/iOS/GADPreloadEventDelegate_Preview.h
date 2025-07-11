//
//  GADPreloadEventDelegate_Preview.h
//  Google Mobile Ads SDK
//
//  Copyright © 2024 Google Inc. All rights reserved.
//

#import "GADPreloadConfiguration_Preview.h"

/// Delegate for preloading events.
@protocol GADPreloadEventDelegate <NSObject>

/// Called when an ad becomes available for the configuration.
- (void)adAvailableForPreloadConfiguration:(nonnull GADPreloadConfiguration *)configuration;

/// Called when the last available ad is exhausted for the configuration.
- (void)adsExhaustedForPreloadConfiguration:(nonnull GADPreloadConfiguration *)configuration;

@end
