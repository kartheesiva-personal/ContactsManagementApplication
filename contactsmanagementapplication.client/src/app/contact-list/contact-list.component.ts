import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ContactService } from '../../Service/contact.service';
import { Contact } from '../../Modal/Contact';
declare var $: any;  // jQuery is required for Bootstrap modal methods
@Component({
  selector: 'app-contact-list',
  templateUrl: 'contact-list.component.html',
  styleUrls: ['contact-list.component.scss']
})
export class ContactListComponent {
  @Input() contacts: any[] = [];
  @Output() deleteContact = new EventEmitter<number>();
  @Output() editContact = new EventEmitter<any>();
  constructor(private contactService: ContactService) { }

  ngOnInit(): void {
    this.contactService.getContacts().subscribe((data) => {
      this.contacts = data;
    });
  }

  loadContacts(): void {
    this.contactService.getContacts().subscribe((data) => {
      this.contacts = data;
    });
  }

  openContactFormModal(): void {
    $('#contactFormModal').modal('show');  // Open the modal when the button is clicked
  }

  closeContactFormModal(): void {
    $('#contactFormModal').modal('hide');  // Close the modal
  }

  reloadContacts(): void {
    this.loadContacts();
    this.closeContactFormModal();  // Close the modal after saving the contact
  }
  // Delete contact
  onDelete(contactId: number) {
    this.deleteContact.emit(contactId);
  }
  //deleteContact(id: number): void {
  //  if (confirm('Are you sure you want to delete this contact?')) {
  //    this.contactService.deleteContact(id).subscribe(() => {
  //      this.contacts = this.contacts.filter(c => c.id !== id);
  //    });
  //  }
  //}

  // Edit contact
  onEdit(contact: any) {
    this.editContact.emit(contact);
  }

  addNewContact(): void {
    // Clear the form or navigate to the contact form for adding a new contact
    console.log('Adding new contact');
  }
}
