import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { map, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate{

  constructor(private authService : AuthService, private router : Router) { }
  canActivate(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    return this.authService.isLoggedIn$.pipe(take(1),
      map(isLoggedIn=>{
        if(isLoggedIn){
          return true;
        }
        else{
          return this.router.createUrlTree(['/login'])
        }
    }))
  }
}
