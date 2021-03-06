﻿namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNET.Api.Contracts.V1;
    using AuthorizeNET.Api.Controllers.Bases;


#pragma warning disable 1591
    public class getAUJobSummaryController : ApiOperationBase<getAUJobSummaryRequest, getAUJobSummaryResponse> {

	    public getAUJobSummaryController(getAUJobSummaryRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    //if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
		    //if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");
		
		    //validate not-required fields		
	    }

        protected override void BeforeExecute()
        {
            var request = GetApiRequest();
        }
    }
#pragma warning restore 1591
}