//
//  GADMobileAds.h
//  Google Mobile Ads SDK
//
//  Copyright 2024 Google LLC. All rights reserved.
//

#import <GoogleMobileAds/GADMobileAds.h>
#import "GADPreloadConfiguration_Preview.h"
#import "GADPreloadEventDelegate_Preview.h"

@interface GADMobileAds ()

/// Starts preloading full screen ads from the configurations.
/// Ad loads and ad expiration events will be forwarded to the delegate provided.
- (void)preloadWithConfigurations:(nonnull NSArray<GADPreloadConfiguration *> *)configurations
                         delegate:(nonnull id<GADPreloadEventDelegate>)delegate;
@end
