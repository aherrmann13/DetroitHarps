import { Component, Input } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: 'dh-register-final-step',
    template: `
    <mat-progress-bar *ngIf='loading' mode='indeterminate'></mat-progress-bar>
    <div *ngIf='!loading' class='register-success'>
        <span class='mat-headline'>Registration Success!</span>
        <br />
        <br />
        <br />
        <span class='mat-subheading-2'>Thank for registering for the 2018 summer program</span>
        <br />
        <br />
        <div *ngIf='paymentType === "paypal"'>
            PayPal (select from menu below and then click Buy Now)
            <form ngNoForm action='https://www.paypal.com/cgi-bin/webscr' method='post' target='_top'>
                <input type='hidden' name='cmd' value='_s-xclick'>
                <input type='hidden' name='hosted_button_id' value='YELLJL3VMKMLC'>
                <table>
                    <tr><td><input type='hidden' name='on0' value='Registration'>Registration</td></tr><tr><td>
                        <select name='os0'>
                            <option value='1 Child'>1 Child $20.00 USD</option>
                            <option value='Family'>Family $30.00 USD</option>
                        </select>
                    </td></tr>
                </table>
                <input type='hidden' name='currency_code' value='USD'>
                <input type='image' src='https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif' border='0' name='submit' alt='PayPal - The safer, easier way to pay online!'>
                <img alt='' border='0' src='https://www.paypalobjects.com/en_US/i/scr/pixel.gif' width='1' height='1'>
            </form>
        </div>
    </div>`,
    styleUrls: [ '../register.component.scss' ]
})
export class FinalStepComponent {
    @Input() loading: boolean = false;
    @Input() paymentType: string;
}