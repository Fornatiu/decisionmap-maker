// header.component.ts
// import { Component, OnInit } from '@angular/core';
// import { WishlistService } from '../../services/wishlist.service';
// import { CartService } from '../../services/cart.service';
// import { AuthService } from '../../services/auth.service';

// @Component({
//   selector: 'app-header',
//   templateUrl: './header.component.html',
//   styleUrls: ['./header.component.css']
// })
// export class HeaderComponent implements OnInit {
//   wishlistLength: number = 0;
//   cartLength: number = 0;
//   loggedInUserId: string = '';
//   isAuthenticated = false;

//   constructor(
//     private wishlistService: WishlistService,
//     private cartService: CartService,
//     private authService: AuthService
//   ) {}

//   ngOnInit(): void {
//     this.loggedInUserId = this.authService.getUserIdFromToken();
//     this.isAuthenticated = this.authService.isAuthenticated();

//     this.wishlistService.wishlistCount$.subscribe((count) => {
//       this.wishlistLength = count;
//     });

//     this.cartService.cartCount$.subscribe((count) => {
//       this.cartLength = count;
//     });

//     this.wishlistService.fetchWishlistCount(this.loggedInUserId);
//     this.cartService.fetchCartCount(this.loggedInUserId);
//   }
// }
