import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from "@angular/router";
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

  constructor(private authService: AuthService){}


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
    console.log('user log out')
  }

}
