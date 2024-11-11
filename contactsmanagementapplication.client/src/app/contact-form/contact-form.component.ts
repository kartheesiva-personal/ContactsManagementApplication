import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../Service/contact.service';
import { Contact } from '../../Modal/Contact';

@Component({
  selector: 'app-contact-form',
  templateUrl: 'contact-form.component.html',
  styleUrls: ['contact-form.component.scss']
})
export class ContactFormComponent {
  @Input() contact: Contact | null = null;
  @Output() contactSaved = new EventEmitter<void>();

  contactForm: FormGroup;

  constructor(private fb: FormBuilder, private contactService: ContactService) {
    this.contactForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnChanges(): void {
    if (this.contact) {
      this.contactForm.patchValue(this.contact);
    }
  }

  save(): void {
    if (this.contactForm.valid) {
      const contact: Contact = this.contactForm.value;
      if (this.contact) {
        this.contactService.updateContact(this.contact.id, contact).subscribe(() => {
          this.contactSaved.emit();
        });
      } else {
        this.contactService.addContact(contact).subscribe(() => {
          this.contactSaved.emit();
        });
      }
    }
  }
}
