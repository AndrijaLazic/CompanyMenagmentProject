import { HttpInterceptorFn } from '@angular/common/http';

export const tokenInterceptor: HttpInterceptorFn = (request, next) => {
  if (request.url.includes('/Login')) {
    return next(request);
  }
  if (sessionStorage.getItem('UserJWT')) {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${sessionStorage.getItem('UserJWT')}`,
      },
    });
  }

  return next(request);
};
