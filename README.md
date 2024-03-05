# BlazorEcommerce

It is a Clothing e-commerce website.

# Prerequisites

You need to install a Visual Studio Latest version 2022.  This project is built on .Net 8.0
# Default Username and Password for Admin
Username admin@live.com
Password Test@123

# How do I build this Repository?

I used the repository pattern here. The data access layer is separated from the business logic.
In the Blazor Server Project, the Admin can perform CRUD operations on the products, categories, and set the price according to the size of the clothes, and manage Orders. Operations for Orders are cancellation, Refund, and shipping. The payment gateway used here is STRIPE.
In the Client Webassembly project, consumers consume data using API's.
I use Blazored Local storage to store customers' carts. and added Microsoft Identity for Authorization and Authentication.




