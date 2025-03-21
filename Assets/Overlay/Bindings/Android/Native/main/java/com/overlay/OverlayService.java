package com.overlay;

import android.app.Service;
import android.content.Intent;
import android.graphics.PixelFormat;
import android.os.IBinder;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.WindowManager;
import android.util.Log;
import android.widget.TextView;
import android.graphics.Color;

public class OverlayService extends Service {
    private static final String TAG = "OverlayService";
    private WindowManager windowManager;
    private View overlayView;

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

   @Override
   public void onCreate() {
        super.onCreate();
        try {
            windowManager = (WindowManager) getSystemService(WINDOW_SERVICE);
            if (windowManager == null) {
                Log.e(TAG, "Failed to get WindowManager service");
                stopSelf();
                return;
            }

            TextView overlayView = new TextView(this);
            overlayView.setText("Overlay");
            overlayView.setPadding(16, 16, 16, 16);
            overlayView.setTextColor(Color.WHITE);
            overlayView.setBackgroundColor(Color.argb(128, 0, 0, 0));

            WindowManager.LayoutParams params = new WindowManager.LayoutParams(
                WindowManager.LayoutParams.WRAP_CONTENT,
                WindowManager.LayoutParams.WRAP_CONTENT,
                WindowManager.LayoutParams.TYPE_APPLICATION_OVERLAY,
                WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE,
                PixelFormat.TRANSLUCENT
            );

            params.gravity = Gravity.TOP | Gravity.START;
            params.x = 0;
            params.y = 100;

            try {
                windowManager.addView(overlayView, params);
                Log.d(TAG, "Overlay view added successfully");
            } catch (Exception e) {
                Log.e(TAG, "Failed to add overlay view", e);
               stopSelf();
            }
        } catch (Exception e) {
            Log.e(TAG, "Error in onCreate", e);
            stopSelf();
        }
   }

    @Override
    public void onDestroy() {
        super.onDestroy();
        if (overlayView != null && windowManager != null) {
            try {
                windowManager.removeView(overlayView);
                Log.d(TAG, "Overlay view removed successfully");
            } catch (Exception e) {
                Log.e(TAG, "Error removing overlay view", e);
            }
        }
    }
}