package md58644c841e34c7f0d4f87f8fe1521f52b;


public class RadExtendedCartesianChartView
	extends com.telerik.widget.chart.visualization.cartesianChart.RadCartesianChartView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.XamarinForms.ChartRenderer.Android.RadExtendedCartesianChartView, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", RadExtendedCartesianChartView.class, __md_methods);
	}


	public RadExtendedCartesianChartView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadExtendedCartesianChartView.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ChartRenderer.Android.RadExtendedCartesianChartView, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public RadExtendedCartesianChartView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadExtendedCartesianChartView.class)
			mono.android.TypeManager.Activate ("Telerik.XamarinForms.ChartRenderer.Android.RadExtendedCartesianChartView, Telerik.XamarinForms.Chart, Version=2018.1.202.240, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);

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
