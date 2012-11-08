using System;

namespace Xenosynth.Web.UI {

    //TODO: Rename CmsFileState

	/// <summary>
	/// The state of a CmsFile in the CMS in its content life cycle.
	/// </summary>
	public enum CmsState {
		Unsaved = 0,

		Unpublished = 100,
		//WaitingApproval = 105
		//Approved = 110,
		//Declined = 120,
		
		Published = 200,
		Archived = 300,
		Deleted = 400
	}
}
