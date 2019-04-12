import { Client, RegisterModel, RegisterParentModel, RegisterContactInformationModel, RegisterChildModel, RegisterChildModelGender } from "../../shared/client/api.client";
import { ParentInformationComponentData } from "./forms/parent-information/parent-information.component";
import { AddressInformationComponentData } from "./forms/address-information/address-information.component";
import { ChildrenInformationComponentData, ChildGender } from "./forms/children-information/children-information.component";
import { PaymentInformationComponentData } from "./forms/payment-information/payment-information.component";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class RegisterService {
    constructor(private _client: Client) { }
    
    // leaving payment param for now to maintain compatability when I add support
    register(
        parentData: ParentInformationComponentData,
        addressData: AddressInformationComponentData,
        childrenData: Array<ChildrenInformationComponentData>,
        paymentData: PaymentInformationComponentData
    ): Observable<void> {

        const model = this.getRegisterModel(parentData, addressData, childrenData);
        return this._client.register(model);
    }

    private getRegisterModel(parentData: ParentInformationComponentData,
        addressData: AddressInformationComponentData,
        childrenData: Array<ChildrenInformationComponentData>
    ): RegisterModel {
        const registrationCreateModel: RegisterModel = new RegisterModel({
            parent: this.getParentInformation(parentData), 
            contactInformation: this.getContactInformation(parentData, addressData),
            children: this.getRegistrationCreateModelChildren(childrenData),
        });
        return registrationCreateModel;
    }
    
    private getParentInformation(parentData: ParentInformationComponentData) : RegisterParentModel {
        return new RegisterParentModel({
            firstName: parentData.parentFirstName,
            lastName: parentData.parentLastName,
        })
    }
    
    private getContactInformation(
        parentData: ParentInformationComponentData,
        addressData: AddressInformationComponentData
    ): RegisterContactInformationModel {
        return new RegisterContactInformationModel({
            email: parentData.emailAddress,
            phoneNumber: parentData.phoneNumber,
            address: addressData.address,
            address2: addressData.address2,
            city: addressData.city,
            state: addressData.state,
            zip: addressData.zip,
        })
    }
    
    private getRegistrationCreateModelChildren(childrenData: Array<ChildrenInformationComponentData>): Array<RegisterChildModel> {
        return childrenData.map(x => new RegisterChildModel({
            firstName: x.childFirstName,
            lastName: x.childLastName,
            gender: x.childGender == ChildGender.Male ? RegisterChildModelGender.Male : RegisterChildModelGender.Female,  
            dateOfBirth: x.childDob,
            shirtSize: x.childShirtSize
        }));
    }
}