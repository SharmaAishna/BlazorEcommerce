﻿redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51OmNHBHU7lUI8NQGf0uJvFIhkPsZBhZDb2oGm25CwwnJMTbZbnnLw7a5YFnVzWASVuixVvXx7yZrO156qNFd0q2I00cxRgyVcD")
    stripe.redirectToCheckout({ sessionId: sessionId });
}