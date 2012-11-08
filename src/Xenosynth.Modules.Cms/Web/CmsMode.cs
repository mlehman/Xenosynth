using System;

namespace Xenosynth.Web {
	/// <summary>
	/// The mode of an http request in the CMS.
	/// </summary>
	public enum CmsMode {
		Edit = 0,
		Unpublished = 100,
		Published = 200
	}
}
