#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Student Performance Prediction System
نظام توقع أداء الطلاب باستخدام الذكاء الاصطناعي

Setup script for the project
"""

from setuptools import setup, find_packages
import os

# Read README file
def read_readme():
    with open("README.md", "r", encoding="utf-8") as fh:
        return fh.read()

# Read requirements
def read_requirements():
    with open("requirements.txt", "r", encoding="utf-8") as fh:
        return [line.strip() for line in fh if line.strip() and not line.startswith("#")]

setup(
    name="student-performance-prediction",
    version="2.0.0",
    author="AI Developer",
    author_email="developer@studentperformance.ai",
    description="نظام ذكي لتوقع أداء الطلاب باستخدام الذكاء الاصطناعي",
    long_description=read_readme(),
    long_description_content_type="text/markdown",
    url="https://github.com/your-username/student-performance-prediction",
    project_urls={
        "Bug Reports": "https://github.com/your-username/student-performance-prediction/issues",
        "Source": "https://github.com/your-username/student-performance-prediction",
        "Documentation": "https://github.com/your-username/student-performance-prediction/tree/main/docs",
    },
    packages=find_packages(),
    classifiers=[
        "Development Status :: 5 - Production/Stable",
        "Intended Audience :: Education",
        "Intended Audience :: Developers",
        "Topic :: Education",
        "Topic :: Scientific/Engineering :: Artificial Intelligence",
        "License :: OSI Approved :: MIT License",
        "Programming Language :: Python :: 3",
        "Programming Language :: Python :: 3.9",
        "Programming Language :: Python :: 3.10",
        "Programming Language :: Python :: 3.11",
        "Operating System :: OS Independent",
        "Framework :: Flask",
        "Natural Language :: Arabic",
        "Natural Language :: English",
    ],
    python_requires=">=3.9",
    install_requires=read_requirements(),
    extras_require={
        "dev": [
            "pytest>=7.0.0",
            "pytest-cov>=4.0.0",
            "black>=22.0.0",
            "flake8>=5.0.0",
            "mypy>=0.991",
        ],
        "docs": [
            "sphinx>=5.0.0",
            "sphinx-rtd-theme>=1.0.0",
        ],
    },
    entry_points={
        "console_scripts": [
            "student-performance=flask_app_enhanced:main",
        ],
    },
    include_package_data=True,
    package_data={
        "": [
            "templates/*.html",
            "data/*.csv",
            "assets/*.png",
            "docs/*.md",
        ],
    },
    keywords=[
        "artificial intelligence",
        "machine learning",
        "education",
        "student performance",
        "prediction",
        "flask",
        "web application",
        "arabic",
        "rtl",
        "bootstrap",
        "scikit-learn",
        "linear regression",
    ],
    zip_safe=False,
)