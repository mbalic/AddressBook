import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { ContactEditComponent } from '../contacts/contact-edit/contact-edit.component';

@Injectable()
export class PreventUnsavedChanges
  implements CanDeactivate<ContactEditComponent> {
  canDeactivate(component: ContactEditComponent) {
    if (component.editForm.dirty) {
      return confirm(
        'Are you sure you want to continue? Any unsaved changes will be lost.'
      );
    }
    return true;
  }
}
