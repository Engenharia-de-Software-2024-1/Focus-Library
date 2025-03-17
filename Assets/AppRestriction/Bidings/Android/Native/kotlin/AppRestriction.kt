package com.apprestriction

import android.content.pm.PackageManager
import android.content.Context
import android.content.pm.ApplicationInfo
import android.graphics.drawable.Drawable
import android.app.ActivityManager

class AppRestriction (
    context: Context
) {
    private val context: Context = context;

    fun getInstalledApps(): List<ApplicationInfo> {
        val apps = context.packageManager.getInstalledApplications(PackageManager.GET_META_DATA)

        return apps.filter { appInfo ->
            (appInfo.flags and ApplicationInfo.FLAG_SYSTEM) == 0 &&
            (appInfo.flags and ApplicationInfo.FLAG_UPDATED_SYSTEM_APP) == 0
        }
    }

    fun getRunningAppsNames(): List<String> {
        val activityManager = context.getSystemService(Context.ACTIVITY_SERVICE) as (ActivityManager)
        val apps = activityManager.getRunningAppProcesses()
        
        return apps.map { appProcessInfo ->
            appProcessInfo.processName
        }
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