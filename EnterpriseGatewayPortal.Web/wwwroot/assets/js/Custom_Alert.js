(function (window, document) {
    function initializeCustomAlerts() {
        const css = `
        .alert-stack-container {
          position: fixed;
          top: 10px;
          left: 50%;
          transform: translateX(-50%);
          z-index: 9999;
          display: flex;
          flex-direction: column;
          gap: 10px;
          max-width: 600px;
          width: 90%;
          font-family: 'Inter', sans-serif;
        }

        .alert {
          display: flex;
          align-items: center;
          justify-content: space-between;
          font-size: 16px;
          padding: 16px 24px;
          border-radius: 4px;
          border: 1px solid;
          box-shadow: 0 2px 6px rgba(0,0,0,0.1);
          user-select: none;
          min-height: 56px;
          opacity: 1;
          transition: opacity 0.4s ease, transform 0.4s ease;
          animation: fadeSlideIn 0.4s ease-out;
        }

        .alert-success {
          background-color: #f3f9f5 !important;
          border-color: #2f5c2f30 !important;
          color: #2f5c2f !important;
        }

        .alert-info {
          background-color: #e3f2fd !important;
          border-color: #31708f30 !important;
          color: #31708f !important;
        }

        .alert-warning {
          background-color: #fff8e1 !important;
          border-color: #8a6d3b30 !important;
          color: #8a6d3b !important;
        }

        .alert-error {
          background-color: #fcebea !important;
          border-color: #a94442 !important;
          color: #a94442 !important;
        }

        .alert-content {
          display: flex;
          align-items: center;
          gap: 8px;
          flex-grow: 1;
          padding-right: 24px;
        }

        .alert img.icon {
          height: 20px;
          width: 20px;
          flex-shrink: 0;
        }

        .alert img.close-btn {
          cursor: pointer;
          height: 20px;
          width: 20px;
          flex-shrink: 0;
        }

        @keyframes fadeSlideIn {
          0% {
            opacity: 0;
            transform: translateY(-20px);
          }
          100% {
            opacity: 1;
            transform: translateY(0);
          }
        }

        @media (max-width: 768px) {
          .alert-stack-container {
            width: 100%;
            left: 0;
            transform: none;
            padding: 0 10px;
          }
          .alert {
            border-radius: 0;
          }
        }
        `;

        const styleEl = document.createElement('style');
        styleEl.appendChild(document.createTextNode(css));

        // This will be safe now
        document.head.appendChild(styleEl);

        const iconMap = {
            success: '/assets/images/Alert_Success_Icon.svg',
            error: '/assets/images/Alert_Error_Icon.svg',
            warning: '/assets/images/Alert_Warning_Icon.svg',
            info: '/assets/images/Alert_Info_Icon.svg'
        };

        const closeIconUrl = '/assets/images/Alert_Success_Close_Icon.svg';

        let stackContainer = document.querySelector('.alert-stack-container');
        if (!stackContainer) {
            stackContainer = document.createElement('div');
            stackContainer.className = 'alert-stack-container';
            document.body.appendChild(stackContainer);
        }

        function showAlert(type = 'success', message = '') {
            type = type.toLowerCase();
            const alertClass = `alert-${type}`;
            const iconUrl = iconMap[type] || iconMap.info;

            const alert = document.createElement('div');
            alert.className = `alert ${alertClass}`;

            const contentWrap = document.createElement('div');
            contentWrap.className = 'alert-content';

            const icon = document.createElement('img');
            icon.src = iconUrl;
            icon.className = 'icon';
            icon.alt = `${type} icon`;

            const msgSpan = document.createElement('span');
            msgSpan.textContent = message;

            const closeBtn = document.createElement('img');
            closeBtn.src = closeIconUrl;
            closeBtn.className = 'close-btn';
            closeBtn.alt = 'Close';
            closeBtn.onclick = () => {
                alert.remove();
            };

            contentWrap.appendChild(icon);
            contentWrap.appendChild(msgSpan);
            alert.appendChild(contentWrap);
            alert.appendChild(closeBtn);
            stackContainer.appendChild(alert);
        }

        window.showAlert = showAlert;
    }

    // Check if DOM is already loaded, otherwise wait for it
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initializeCustomAlerts);
    } else {
        initializeCustomAlerts(); // DOM already loaded
    }
})(window, document);
