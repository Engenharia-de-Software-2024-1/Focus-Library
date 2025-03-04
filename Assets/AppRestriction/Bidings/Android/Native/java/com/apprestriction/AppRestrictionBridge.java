package com.apprestriction;

import com.apprestriction.AppRestriction;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import java.util.List;

public class AppRestrictionBridge {
    private static AppRestriction appRestriction;

    public static void voidInit(Context context) {
        if (appRestriction != null) {
            return;
        }
        appRestriction = new AppRestriction(context);
    }
    
    public static List<ApplicationInfo> getInstalledApps() {
        return appRestriction.getInstalledApps();
    }
}