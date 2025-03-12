package com.apprestriction

import android.content.pm.PackageManager
import android.content.Context
import android.content.pm.ApplicationInfo
import android.graphics.drawable.Drawable

class AppRestriction (
    context: Context
) {
    private val context: Context = context;

    fun getInstalledApps(): List<ApplicationInfo> {
        return context.packageManager.getInstalledApplications(PackageManager.GET_META_DATA)
    }

    fun getAppName(appInfo: ApplicationInfo): String {
        return context.packageManager.getApplicationLabel(appInfo).toString()
    }

    fun getAppIcon(appInfo: ApplicationInfo): Drawable {
        return context.packageManager.getApplicationIcon(appInfo)
    }

    fun getAppProcessName(appInfo: ApplicationInfo): String {
        return appInfo.processName
    }
}