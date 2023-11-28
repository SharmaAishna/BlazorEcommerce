# BlazorEcommerce

It is a Clothing e-commerce website.

# Prerequisites

You need to install a Visual Studio Latest version 2022.  This project is built on .Net 7.0

# How do I build this Repository?

I used the repository pattern here. The data access layer is separated from the business logic.
In Blazor Server Project, the Admin can perform CRUD operations on the products, categories, and set the price according to the size of the clothes.
In the Client Webassembly project, consumers consume data using API's.
I use Blazored Local storage to store customers' carts. and added Microsoft Identity for Authorization and Authentication.
Payment gateway and deployment of database to Azure are Currently in progress..


