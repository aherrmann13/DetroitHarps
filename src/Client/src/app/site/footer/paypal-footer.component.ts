import { Component } from '@angular/core';

@Component({
  selector: 'dh-paypal-footer',
  templateUrl: './paypal-footer.component.html',
  styleUrls: ['./paypal-footer.component.scss']
})
export class PaypalFooterComponent {
  displayCampRegistration = new Date().getTime() < new Date(2019, 7, 30).getTime();
}
