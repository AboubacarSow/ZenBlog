import { AfterViewInit, Component, OnInit } from '@angular/core';
import { RouterOutlet } from "@angular/router";

@Component({
  selector: 'app-default',
  imports: [RouterOutlet],
  templateUrl: './default.component.html',
  styleUrl: './default.component.css'
})
export class DefaultComponent implements OnInit , AfterViewInit {
  isMobileMenuOpen: boolean = false;
isScrollTopVisible: any;

  ngOnInit(): void {
    window.addEventListener('scroll', () => {
      const scrollTop = document.querySelector('.scroll-top');
      if (scrollTop) {
        if (window.scrollY > 100) {
          scrollTop.classList.add('active');
        } else {
          scrollTop.classList.remove('active');
        }
      }
    });
  }
  ngAfterViewInit(): void {
    // Preloader
    const preloader = document.querySelector('#preloader');
    if (preloader) {
      preloader.remove();
    }
  }

  scrollToTop(event: Event) {
    event.preventDefault();
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  }
  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
    const navmenu = document.querySelector('#navmenu');
    if (navmenu) {
      if (this.isMobileMenuOpen) {
        navmenu.classList.add('mobile-nav-active');
      } else {
        navmenu.classList.remove('mobile-nav-active');
      }
    }
  }

}
