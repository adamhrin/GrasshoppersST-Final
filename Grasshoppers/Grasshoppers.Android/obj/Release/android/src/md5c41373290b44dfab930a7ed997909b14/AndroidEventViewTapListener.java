package md5c41373290b44dfab930a7ed997909b14;


public class AndroidEventViewTapListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.telerik.widget.calendar.dayview.CalendarDayView.EventViewTapListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEventViewTap:(Lcom/telerik/widget/calendar/events/Event;)V:GetOnEventViewTap_Lcom_telerik_widget_calendar_events_Event_Handler:Com.Telerik.Widget.Calendar.Dayview.CalendarDayView/IEventViewTapListenerInvoker, Telerik.Xamarin.Android.Input\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventViewTapListener, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", AndroidEventViewTapListener.class, __md_methods);
	}


	public AndroidEventViewTapListener ()
	{
		super ();
		if (getClass () == AndroidEventViewTapListener.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventViewTapListener, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public AndroidEventViewTapListener (md5c41373290b44dfab930a7ed997909b14.CalendarRenderer p0)
	{
		super ();
		if (getClass () == AndroidEventViewTapListener.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.InputRenderer.Android.AndroidEventViewTapListener, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Telerik.XamarinForms.InputRenderer.Android.CalendarRenderer, Telerik.XamarinForms.Input, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onEventViewTap (com.telerik.widget.calendar.events.Event p0)
	{
		n_onEventViewTap (p0);
	}

	private native void n_onEventViewTap (com.telerik.widget.calendar.events.Event p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
