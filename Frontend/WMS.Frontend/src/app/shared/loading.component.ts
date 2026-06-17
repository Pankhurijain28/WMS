import { Component } from '@angular/core';

@Component({
  selector: 'app-loading',
  standalone: true,
  template: `
    <div class="text-center p-5">
      Loading...
    </div>
  `
})
export class LoadingComponent {}