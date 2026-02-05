import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from "@angular/router";
import { AuthService } from '../../core/services/auth.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-admin',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit{
  pageTitle: string= 'Blog Management';
  username : string = ''

  constructor(private authService: AuthService,private router : Router){}


  ngOnInit(): void {
    this.authService.currentUser$.pipe(take(1)).subscribe(currentUser=>{
      this.username = currentUser.fullname;
      console.log(this.username)
      console.log(currentUser)
    })
  }

  getUserName() {
    return 'John Carlson'
  }
  logout() {
    this.authService.logout()
    this.router.navigate(['/login'])
    //window.location.reload()
    console.log('user log out')
  }

}
