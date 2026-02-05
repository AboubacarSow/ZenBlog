import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    importProvidersFrom([JwtModule.forRoot({
      config: {
        tokenGetter:()=> localStorage.getItem('accessToken'),
        allowedDomains:['localhost:5141']
      }
    })]),
    provideHttpClient(withInterceptorsFromDi()),
  ]
};
