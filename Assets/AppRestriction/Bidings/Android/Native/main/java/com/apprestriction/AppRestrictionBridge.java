package com.apprestriction;

import com.apprestriction.AppRestriction;
import android.app.Activity;
import android.content.pm.ApplicationInfo;
import java.io.ByteArrayOutputStream;
import java.util.List;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.PixelFormat;

public class AppRestrictionBridge {
    private static AppRestriction appRestriction;

    public static void Init(Activity activity) {
        if (appRestriction != null) {
            return;
        }
        appRestriction = new AppRestriction(activity);
    }
    
    public static List<ApplicationInfo> getInstalledApps() {
        return appRestriction.getInstalledApps();
    }

    public static List<String> getRunningAppsNames() {
        return appRestriction.getRunningAppsNames();
    }

    public static String getAppName(ApplicationInfo appInfo) {
        return appRestriction.getAppName(appInfo);
    }

    public static byte[] getAppIcon(ApplicationInfo appInfo) {
        var drawable = appRestriction.getAppIcon(appInfo);

        Bitmap bitmap;
        if (drawable instanceof BitmapDrawable) {
            bitmap = ((BitmapDrawable)drawable).getBitmap();
        } else {
            int width = drawable.getIntrinsicWidth();
            width = width > 0 ? width : 1;
            int height = drawable.getIntrinsicHeight();
            height = height > 0 ? height : 1;
        
            bitmap = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);
            Canvas canvas = new Canvas(bitmap);
            drawable.setBounds(0, 0, canvas.getWidth(), canvas.getHeight());
            drawable.draw(canvas);
        }
    
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 100, baos);
        return baos.toByteArray();
    }

    public static String getAppProcessName(ApplicationInfo appInfo) {
        return appRestriction.getAppProcessName(appInfo);
    }
}