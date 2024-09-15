import { provideHttpClient, withInterceptors, withFetch } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { provideClientHydration } from "@angular/platform-browser";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { authInterceptor } from "./core/interceptors/auth.interceptor";
import { errorInterceptor } from "./core/interceptors/error.interceptor";
import { PaginationConfig } from "ngx-bootstrap/pagination";


@NgModule({
    providers: [
        provideClientHydration(),
        provideHttpClient(withInterceptors([authInterceptor, errorInterceptor]), withFetch()),
        PaginationConfig,
        provideAnimationsAsync()
      ],
})
export class CoreModule{}