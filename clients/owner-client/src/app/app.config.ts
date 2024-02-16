import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { GALLERY_CONFIG, GalleryConfig } from 'ng-gallery';
import { LIGHTBOX_CONFIG, LightboxConfig } from 'ng-gallery/lightbox';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';
import { errorInterceptor } from './_interceptors/error.interceptor';
import { provideToastr } from 'ngx-toastr';
import { loadingInterceptor } from './_interceptors/loading.interceptor';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    importProvidersFrom(
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
        }
      })
    ),
    provideAnimations(),
    provideToastr(),
    provideHttpClient(
      withInterceptors([jwtInterceptor, errorInterceptor, loadingInterceptor])
    ),
    {
      provide: GALLERY_CONFIG,
      useValue: {
        autoHeight: true,
        imageSize: 'cover',
      } as GalleryConfig,
    },
    {
      provide: LIGHTBOX_CONFIG,
      useValue: {
        keyboardShortcuts: false,
        exitAnimationTime: 1000,
      } as LightboxConfig,
    },
  ],
};
