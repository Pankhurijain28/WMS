import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = () => {

  const token =
    localStorage.getItem('token');

  if (token)
    return true;

  return inject(Router)
    .createUrlTree(['/']);
};